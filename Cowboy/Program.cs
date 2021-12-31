using Cowboy;
using Discord;
using Discord.WebSocket;

// Our main Discord client.
DiscordSocketClient _client = new();

// Register all slash commands.
var _slashCommands = new Dictionary<string, ISlashCommand>()
    .AddCommand(new RollCommand())
    .AddCommand(new FlipCoinCommand());

_client.Log += Log;
_client.Ready += Ready;
_client.SlashCommandExecuted += SlashCommandExecuted;

var token = Environment.GetEnvironmentVariable("CowboyDiscordToken", EnvironmentVariableTarget.User);

await _client.LoginAsync(TokenType.Bot, token);
await _client.StartAsync();

// Block this task until the program is closed.
await Task.Delay(-1);

static Task Log(LogMessage message)
{
    Console.WriteLine(message.ToString());
    return Task.CompletedTask;
}

async Task Ready()
{
#if !DEBUG
    foreach (var command in _slashCommands.Values)
    {
        await _client.CreateGlobalApplicationCommandAsync(command.Build());
    }
#else
    var guildId = Convert.ToUInt64(Environment.GetEnvironmentVariable("CowboyDiscordGuildId", EnvironmentVariableTarget.User));
    var guild = _client.GetGuild(guildId);

    foreach (var command in _slashCommands.Values)
    {
        await guild.CreateApplicationCommandAsync(command.Build());
    }
#endif
}

async Task SlashCommandExecuted(SocketSlashCommand command)
{
    // Execute the slash command with the passed name, if there is one.
    if (_slashCommands.ContainsKey(command.Data.Name))
    {
        await _slashCommands[command.Data.Name].ExecuteAsync(command);
    }
}
