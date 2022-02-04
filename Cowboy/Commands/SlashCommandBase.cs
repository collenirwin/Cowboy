using Cowboy.Commands.Received;
using Discord;

namespace Cowboy.Commands;

/// <summary>
/// Base class for all slash commands.
/// </summary>
internal abstract class SlashCommandBase : ISlashCommand
{
    /// <inheritdoc />
    public abstract string Name { get; }

    /// <inheritdoc />
    public abstract string Description { get; }

    /// <inheritdoc />
    public abstract Task ExecuteAsync(IReceivedCommand command);

    /// <inheritdoc />
    public virtual SlashCommandProperties Build()
    {
        return new SlashCommandBuilder()
            .WithName(Name)
            .WithDescription(Description)
            .Build();
    }
}
