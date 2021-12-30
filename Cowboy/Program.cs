using Cowboy;
using Discord;
using Discord.WebSocket;

DiscordSocketClient _client = new();
Dictionary<string, ISlashCommand> _slashCommands = new();

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
    var rollCommand = new RollCommand();
    _slashCommands.Add(rollCommand.Name, rollCommand);

#if !DEBUG
    await _client.CreateGlobalApplicationCommandAsync(rollCommand.Build());
#else
    var guildId = Convert.ToUInt64(Environment.GetEnvironmentVariable("CowboyDiscordGuildId", EnvironmentVariableTarget.User));
    var guild = _client.GetGuild(guildId);

    await guild.CreateApplicationCommandAsync(rollCommand.Build());
#endif
}

async Task SlashCommandExecuted(SocketSlashCommand command)
{
    if (_slashCommands.ContainsKey(command.Data.Name))
    {
        await _slashCommands[command.Data.Name].ExecuteAsync(command);
    }
}
