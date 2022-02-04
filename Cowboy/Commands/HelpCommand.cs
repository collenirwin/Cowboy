using Cowboy.Commands.Received;

namespace Cowboy.Commands;

/// <summary>
/// Slash command to list all other slash commands.
/// </summary>
internal class HelpCommand : SlashCommandBase
{
    /// <inheritdoc />
    public override string Name => "cowboy-help";

    /// <inheritdoc />
    public override string Description => "Lists all available Cowboy commands";

    /// <inheritdoc />
    public override async Task ExecuteAsync(IReceivedCommand command)
    {
        var commandHelpEntries = SlashCommands.All.Values
            .OrderBy(command => command.Name)
            .Select(command => $"> `/{command.Name}` - {command.Description}");

        var message = $"**Cowboy Commands**" +
            Environment.NewLine +
            string.Join(Environment.NewLine, commandHelpEntries);

        await command.RespondAsync(text: message);
    }
}
