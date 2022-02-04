using Cowboy.Commands.Received;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.UnitTests;

internal class PropReceivedCommand : IReceivedCommand
{
    private readonly IList<string?> _output = new List<string?>();

    public IReadOnlyCollection<string?> Output => _output.ToArray();

    public IReadOnlyCollection<IApplicationCommandInteractionDataOption> Options { get; }

    public PropReceivedCommand(IReadOnlyCollection<IApplicationCommandInteractionDataOption>? options = null)
    {
        Options = options ?? Array.Empty<IApplicationCommandInteractionDataOption>();
    }

    public Task RespondAsync(
        string? text = null,
        Embed[]? embeds = null,
        bool isTTS = false,
        bool ephemeral = false,
        AllowedMentions? allowedMentions = null,
        MessageComponent? components = null,
        Embed? embed = null,
        RequestOptions? options = null)
    {
        _output.Add(text);
        return Task.CompletedTask;
    }
}
