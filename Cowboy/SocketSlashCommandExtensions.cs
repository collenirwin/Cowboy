using Discord.WebSocket;

namespace Cowboy;

internal static class SocketSlashCommandExtensions
{
    public static int? GetOptionalIntArgument(this SocketSlashCommand command, string name)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(name);

        var value = command.Data.Options.FirstOrDefault(option => option.Name == name)?.Value;

        return value switch
        {
            int i => i,
            long l => (int)l,
            null => null,
            _ => throw new NotSupportedException($"Argument for {name} is of an unsupported type.")
        };
    }
}
