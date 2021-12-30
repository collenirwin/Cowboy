using Discord;
using Discord.WebSocket;

namespace Cowboy;

internal class RollCommand : ISlashCommand
{
    private static readonly Random _random = new();

    public string Name => "roll";

    public string Description => "Rolls a random number (defaults to between 1 and 10).";

    public async Task ExecuteAsync(SocketSlashCommand command)
    {
        var low = command.GetOptionalIntArgument("low") ?? 1;
        var high = command.GetOptionalIntArgument("high") ?? 10;

        var result = low > high
            ? "Check yourself and try again."
            : _random.Next(low, high + 1).ToString();

        var embedBuilder = new EmbedBuilder()
            .WithTitle($"Roll between {low} and {high}:")
            .WithDescription(result);

        await command.RespondAsync(embed: embedBuilder.Build());
    }
}
