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

    var rollCommandBuilder = new SlashCommandBuilder()
        .WithName(rollCommand.Name)
        .WithDescription(rollCommand.Description)
        .AddOption(new SlashCommandOptionBuilder()
            .WithName("low")
            .WithDescription("Lower inclusive bound.")
            .WithType(ApplicationCommandOptionType.Integer)
            .WithRequired(false))
        .AddOption(new SlashCommandOptionBuilder()
            .WithName("high")
            .WithDescription("Upper inclusive bound.")
            .WithType(ApplicationCommandOptionType.Integer)
            .WithRequired(false));

    _slashCommands.Add(rollCommand.Name, rollCommand);

    await _client.CreateGlobalApplicationCommandAsync(rollCommandBuilder.Build());
}

async Task SlashCommandExecuted(SocketSlashCommand command)
{
    if (_slashCommands.ContainsKey(command.Data.Name))
    {
        await _slashCommands[command.Data.Name].ExecuteAsync(command);
    }
}
