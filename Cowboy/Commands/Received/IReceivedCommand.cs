using Discord;

namespace Cowboy.Commands.Received;

/// <summary>
/// Represents a command received from a Discord user.
/// </summary>
internal interface IReceivedCommand
{
    /// <summary>
    /// Collection of name-value items representing options passed with the command.
    /// </summary>
    public IReadOnlyCollection<IApplicationCommandInteractionDataOption> Options { get; }

    /// <summary>
    /// Responds to an Interaction with type <see cref="InteractionResponseType.ChannelMessageWithSource"/>.
    /// </summary>
    /// <param name="text">The text of the message to be sent.</param>
    /// <param name="embeds">A array of embeds to send with this response. Max 10.</param>
    /// <param name="isTTS">
    /// <see langword="true"/> if the message should be read out by a text-to-speech reader,
    /// otherwise <see langword="false"/>.
    /// </param>
    /// <param name="ephemeral">
    /// <see langword="true"/> if the response should be hidden to everyone besides the invoker of the command,
    /// otherwise <see langword="false"/>.
    /// </param>
    /// <param name="allowedMentions">The allowed mentions for this response.</param>
    /// <param name="components">A <see cref="MessageComponent"/> to be sent with this response.</param>
    /// <param name="embed">
    /// A single embed to send with this response. If this is passed alongside an array of embeds, 
    /// the single embed will be ignored.
    /// </param>
    /// <param name="options">The request options for this response.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Message content is too long, length must be less or equal to <see cref="DiscordConfig.MaxMessageSize"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// The parameters provided were invalid or the token was invalid.
    /// </exception>
    public Task RespondAsync(
        string? text = null,
        Embed[]? embeds = null,
        bool isTTS = false,
        bool ephemeral = false,
        AllowedMentions? allowedMentions = null,
        MessageComponent? components = null,
        Embed? embed = null,
        RequestOptions? options = null);
}
