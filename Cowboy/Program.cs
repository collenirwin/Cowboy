using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using Cowboy.Commands;
using Discord;
using Discord.WebSocket;

await new CliApplicationBuilder()
    .AddCommand<MainCommand>()
    .Build()
    .RunAsync();

[Command]
class MainCommand : ICommand
{
    private static readonly DiscordSocketClient _client = new();

    [CommandOption(
        "token", 't',
        IsRequired = true,
        EnvironmentVariable = "CowboyDiscordToken",
        Description = "Discord application token string.")]
    public string? DiscordToken { get; init; }

    [CommandOption(
        "guild", 'g',
        IsRequired = false,
        EnvironmentVariable = "CowboyDiscordGuildId",
        Description = "Discord guild ID. Slash commands will be registered globally if this is not provided.")]
    public ulong? DiscordGuildId { get; init; }

    public async ValueTask ExecuteAsync(IConsole console)
    {
        _client.Log += OnLogAsync;
        _client.Ready += OnReadyAsync;
        _client.SlashCommandExecuted += OnSlashCommandExecutedAsync;

        await _client.LoginAsync(TokenType.Bot, DiscordToken);
        await _client.StartAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }

    private static Task OnLogAsync(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }

    private async Task OnReadyAsync()
    {
        if (DiscordGuildId is not null)
        {
            var guild = _client.GetGuild((ulong)DiscordGuildId) ?? throw new Exception("Guild cannot be found.");
            await SlashCommands.RegisterCommandsForGuildAsync(guild);
        }
        else
        {
            await SlashCommands.RegisterCommandsGloballyAsync(_client);
        }
    }

    private static async Task OnSlashCommandExecutedAsync(SocketSlashCommand command)
    {
        await SlashCommands.ExecuteIfExistsAsync(command);
    }
}
