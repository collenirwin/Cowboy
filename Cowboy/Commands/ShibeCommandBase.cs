using Cowboy.Commands.Received;
using Cowboy.Utils;
using System.Net.Http.Json;

namespace Cowboy.Commands;

/// <summary>
/// Base class for slash commands that interact with shibe.online.
/// </summary>
internal abstract class ShibeCommandBase : RateLimitedSlashCommand
{
    private const string _apiUrlPrefix = "https://shibe.online/api/";

    /// <summary>
    /// The shibe.online endpoint (shibes, cats, or birds).
    /// </summary>
    protected abstract string Endpoint { get; }

    /// <inheritdoc />
    protected override TimeSpan TimeBetweenExecutions => TimeSpan.FromMilliseconds(200);

    /// <inheritdoc />
    public override async Task ExecuteAsync(IReceivedCommand command)
    {
        try
        {
            await base.ExecuteAsync(command);
        }
        catch (RateLimitExceededException)
        {
            await command.RespondAsync(text: _rateLimitErrorMessage);
        }

        var apiUrl = $"{_apiUrlPrefix}{Endpoint}?count=1";
        var imageUrls = await Http.Client.GetFromJsonAsync<IEnumerable<string>>(apiUrl)
            ?? throw new Exception($"JSON from {apiUrl} not in expected format.");

        await command.RespondAsync(text: imageUrls.First());
    }
}
