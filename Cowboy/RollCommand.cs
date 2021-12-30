using Discord;
using Discord.WebSocket;

namespace Cowboy;

internal class RollCommand : ISlashCommand
{
    public string Name => "roll";

    public string Description => "Rolls a random number (defaults to between 1 and 10)";

    public async Task ExecuteAsync(SocketSlashCommand command)
    {
        var low = command.GetOptionalIntArgument("low") ?? 1;
        var high = command.GetOptionalIntArgument("high") ?? 10;

        if (low > high)
        {
            throw new InvalidOperationException("Low cannot be greater than high.");
        }

        var result = Random.Shared.Next(low, high + 1);
        var message = $"Roll between **{low}** and **{high}**:{Environment.NewLine}> **{result}**";

        await command.RespondAsync(text: message);
    }

    public SlashCommandProperties Build()
    {
        return new SlashCommandBuilder()
            .WithName(Name)
            .WithDescription(Description)
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("low")
                .WithDescription("Lower inclusive bound")
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(false))
            .AddOption(new SlashCommandOptionBuilder()
                .WithName("high")
                .WithDescription("Upper inclusive bound")
                .WithType(ApplicationCommandOptionType.Integer)
                .WithRequired(false))
            .Build();
    }
}
