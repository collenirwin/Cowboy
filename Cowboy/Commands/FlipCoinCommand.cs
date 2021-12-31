using Discord;
using Discord.WebSocket;

namespace Cowboy.Commands;

/// <summary>
/// Slash command to flip a coin and display the result.
/// </summary>
internal class FlipCoinCommand : ISlashCommand
{
    /// <inheritdoc />
    public string Name => "flip-coin";

    /// <inheritdoc />
    public string Description => "Flips a coin (heads or tails)";

    /// <inheritdoc />
    public async Task ExecuteAsync(SocketSlashCommand command)
    {
        var result = Random.Shared.Next(2) == 0 ? "Heads" : "Tails";
        var message = $"Coin flip:{Environment.NewLine}> **{result}**";

        await command.RespondAsync(text: message);
    }

    /// <inheritdoc />
    public SlashCommandProperties Build()
    {
        return new SlashCommandBuilder()
            .WithName(Name)
            .WithDescription(Description)
            .Build();
    }
}
