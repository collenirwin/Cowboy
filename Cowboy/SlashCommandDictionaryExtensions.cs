namespace Cowboy;

internal static class SlashCommandDictionaryExtensions
{
    public static Dictionary<string, ISlashCommand> AddCommand<TCommand>(
        this Dictionary<string, ISlashCommand> dictionary,
        TCommand command)
        where TCommand : ISlashCommand
    {
        dictionary.Add(command.Name, command);
        return dictionary;
    }
}
