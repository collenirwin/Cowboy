using Cowboy.Commands.Received;
using Cowboy.Utils;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Cowboy.Commands;

/// <summary>
/// Slash command to fetch a random link from the Wikipedia API, and display it.
/// </summary>
internal class RandomWikiPageCommand : RateLimitedSlashCommand
{
    private const string _apiUrl = "https://en.wikipedia.org/api/rest_v1/page/random/title";
    private const string _wikiPageUrlPrefix = "https://en.wikipedia.org/wiki/";

    /// <inheritdoc />
    public override string Name => "random-wiki";

    /// <inheritdoc />
    public override string Description => "Gets a link to a random Wikipedia page";

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

        using var request = new HttpRequestMessage(HttpMethod.Get, _apiUrl);

        // user-agent headers required by the api
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue("CowboyDiscordBot", "1.0"));
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue("(+https://github.com/collenirwin/Cowboy)"));

        var response = await Http.Client.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        using var document = JsonDocument.Parse(json);

        // schema: { "items": [ { "title": "Hi", ... } ] }
        var title = document.RootElement
            .GetProperty("items")
            .EnumerateArray()
            .First()
            .GetProperty("title")
            .GetString();

        // the title here works as the Wikipedia page name
        await command.RespondAsync(text: _wikiPageUrlPrefix + title);
    }
}
