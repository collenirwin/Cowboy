using Cowboy.Utils;
using Discord.WebSocket;

namespace Cowboy.Commands;

/// <summary>
/// Manages all <see cref="ISlashCommand"/>s.
/// </summary>
internal static class SlashCommands
{
    /// <summary>
    /// All <see cref="ISlashCommand"/>s, keyed by <see cref="ISlashCommand.Name"/>.
    /// </summary>
    public static IReadOnlyDictionary<string, ISlashCommand> All { get; }

    static SlashCommands()
    {
        All = new Dictionary<string, ISlashCommand>()
            .AddCommand(new HelpCommand())
            .AddCommand(new RollCommand())
            .AddCommand(new FlipCoinCommand())
            .AddCommand(new RandomWikiPageCommand());
    }

    /// <summary>
    /// Registers all slash commands to the specified guild.
    /// </summary>
    /// <param name="guild">Guild to register the commands to.</param>
    public static async Task RegisterCommandsForGuildAsync(SocketGuild guild)
    {
        foreach (var command in All.Values)
        {
            await guild.CreateApplicationCommandAsync(command.Build());
        }
    }

    /// <summary>
    /// Registers all slash commands globally.
    /// </summary>
    /// <param name="client">Discord client to use for registration.</param>
    public static async Task RegisterCommandsGloballyAsync(DiscordSocketClient client)
    {
        foreach (var command in All.Values)
        {
            await client.CreateGlobalApplicationCommandAsync(command.Build());
        }
    }

    /// <summary>
    /// Executes the slash command received from the <see cref="SocketSlashCommand"/>,
    /// if it exists in <see cref="All"/>.
    /// </summary>
    /// <param name="command">Received command.</param>
    public static async Task ExecuteIfExistsAsync(SocketSlashCommand command)
    {
        if (All.ContainsKey(command.Data.Name))
        {
            await All[command.Data.Name].ExecuteAsync(command);
        }
    }
}
