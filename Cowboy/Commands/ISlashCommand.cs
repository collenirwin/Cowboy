using Discord;
using Discord.WebSocket;

namespace Cowboy.Commands;

/// <summary>
/// Represents a Discord Application command (or slash command).
/// </summary>
internal interface ISlashCommand
{
    /// <summary>
    /// The name of the command (what the user types after the /).
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Lets the user know what the command does.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="command">The command received from Discord (includes arguments if any).</param>
    Task ExecuteAsync(SocketSlashCommand command);

    /// <summary>
    /// Builds this command into its properties.
    /// </summary>
    /// <returns>The built command properties.</returns>
    SlashCommandProperties Build();
}
