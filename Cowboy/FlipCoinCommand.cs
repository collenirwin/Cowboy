using Discord;
using Discord.WebSocket;

namespace Cowboy;

internal class FlipCoinCommand : ISlashCommand
{
    public string Name => "flip-coin";

    public string Description => "Flips a coin (heads or tails)";

    public async Task ExecuteAsync(SocketSlashCommand command)
    {
        var result = Random.Shared.Next(2) == 0 ? "Heads" : "Tails";
        var message = $"Coin flip:{Environment.NewLine}> **{result}**";

        await command.RespondAsync(text: message);
    }

    public SlashCommandProperties Build()
    {
        return new SlashCommandBuilder()
            .WithName(Name)
            .WithDescription(Description)
            .Build();
    }
}
