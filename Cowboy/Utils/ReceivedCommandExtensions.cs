using Cowboy.Commands.Received;

namespace Cowboy.Utils;

/// <summary>
/// Contains extension methods for <see cref="IReceivedCommand"/> objects.
/// </summary>
internal static class ReceivedCommandExtensions
{
    /// <summary>
    /// Gets an optional integer argument passed to a slash command.
    /// </summary>
    /// <param name="command">Command to get the argument from.</param>
    /// <param name="name">Parameter name.</param>
    /// <returns>The argument value.</returns>
    /// <exception cref="NotSupportedException">Thrown if the argument value is of an unsupported type.</exception>
    public static int? GetOptionalIntArgument(this IReceivedCommand command, string name)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(name);

        var value = command.Options.FirstOrDefault(option => option.Name == name)?.Value;

        return value switch
        {
            int i => i,
            long l => (int)l,
            null => null,
            _ => throw new NotSupportedException($"Argument for {name} is of an unsupported type.")
        };
    }
}
