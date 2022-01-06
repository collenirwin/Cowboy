using Discord.WebSocket;

namespace Cowboy.Commands;

/// <summary>
/// Slash command to flip a coin and display the result.
/// </summary>
internal class FlipCoinCommand : SlashCommandBase
{
    /// <inheritdoc />
    public override string Name => "flip-coin";

    /// <inheritdoc />
    public override string Description => "Flips a coin (heads or tails)";

    /// <inheritdoc />
    public override async Task ExecuteAsync(SocketSlashCommand command)
    {
        var result = Random.Shared.Next(2) == 0 ? "Heads" : "Tails";
        var message = $"Coin flip:{Environment.NewLine}> **{result}**";

        await command.RespondAsync(text: message);
    }
}
