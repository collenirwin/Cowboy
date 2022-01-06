using Discord;
using Discord.WebSocket;

namespace Cowboy.Commands;

/// <summary>
/// Slash command to list all other slash commands.
/// </summary>
internal class HelpCommand : ISlashCommand
{
    /// <inheritdoc />
    public string Name => "cowboy-help";

    /// <inheritdoc />
    public string Description => "Lists all available Cowboy commands";

    /// <inheritdoc />
    public async Task ExecuteAsync(SocketSlashCommand command)
    {
        var commandHelpEntries = SlashCommands.All.Values
            .OrderBy(command => command.Name)
            .Select(command => $"> `/{command.Name}` - {command.Description}");

        var message = $"**Cowboy Commands**" +
            Environment.NewLine +
            string.Join(Environment.NewLine, commandHelpEntries);

        await command.RespondAsync(text: message);
    }

    /// <inheritdoc />
    public SlashCommandProperties Build()
    {
        return new SlashCommandBuilder()
            .WithName(Name)
            .WithDescription(Description)
            .Build();
    }
}
