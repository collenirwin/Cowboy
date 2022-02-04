using Discord;
using System;
using System.Collections.Generic;

namespace Cowboy.UnitTests;

internal class CommandOption : IApplicationCommandInteractionDataOption
{
    public string Name { get; }

    public object Value { get; }

    public ApplicationCommandOptionType Type => throw new NotImplementedException();

    public IReadOnlyCollection<IApplicationCommandInteractionDataOption> Options => throw new NotImplementedException();

    public CommandOption(string name, object value)
    {
        Name = name;
        Value = value;
    }
}
