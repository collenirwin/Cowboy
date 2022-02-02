using Discord;
using Discord.WebSocket;

namespace Cowboy.Commands.Received;

/// <summary>
/// Wrapper for a <see cref="SocketSlashCommand"/>.
/// </summary>
internal class ReceivedCommand : IReceivedCommand
{
    private readonly SocketSlashCommand _command;

    /// <inheritdoc />
    public IReadOnlyCollection<IApplicationCommandInteractionDataOption> Options => _command.Data.Options;

    /// <summary>
    /// Initializes a <see cref="ReceivedCommand"/> with a <see cref="SocketSlashCommand"/> to wrap.
    /// </summary>
    /// <param name="command"><see cref="SocketSlashCommand"/> to wrap.</param>
    public ReceivedCommand(SocketSlashCommand command)
    {
        _command = command;
    }

    /// <inheritdoc />
    public async Task RespondAsync(
        string? text = null,
        Embed[]? embeds = null,
        bool isTTS = false,
        bool ephemeral = false,
        AllowedMentions? allowedMentions = null,
        MessageComponent? components = null,
        Embed? embed = null,
        RequestOptions? options = null)
    {
        await _command.RespondAsync(text, embeds, isTTS, ephemeral, allowedMentions, components, embed, options);
    }
}
