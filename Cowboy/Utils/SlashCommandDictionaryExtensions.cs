using Cowboy.Commands;

namespace Cowboy.Utils;

/// <summary>
/// Contains extension methods for dictionaries of slash commands, keyed by <see cref="ISlashCommand.Name"/>.
/// </summary>
internal static class SlashCommandDictionaryExtensions
{
    /// <summary>
    /// Adds a <see cref="ISlashCommand"/> to the dictionary.
    /// </summary>
    /// <typeparam name="TCommand">Type of the slash command.</typeparam>
    /// <param name="dictionary">Dictionary to add to.</param>
    /// <param name="command">Slash command to add.</param>
    /// <returns>The dictionary.</returns>
    public static Dictionary<string, ISlashCommand> AddCommand<TCommand>(
        this Dictionary<string, ISlashCommand> dictionary,
        TCommand command)
        where TCommand : ISlashCommand
    {
        dictionary.Add(command.Name, command);
        return dictionary;
    }
}
