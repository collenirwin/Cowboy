using Cowboy.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cowboy.UnitTests;

public class ShibeApi
{
    private const string _urlPrefix = "https://cdn.shibe.online/";

    [Fact]
    public async Task OutputsShibeUrl()
    {
        var command = new RandomShibeCommand();
        var received = new PropReceivedCommand();

        await command.ExecuteAsync(received);

        Assert.Single(
            received.Output,
            output => output is not null && output.StartsWith(_urlPrefix + "shibes/"));
    }

    [Fact]
    public async Task OutputsBirdUrl()
    {
        var command = new RandomBirdCommand();
        var received = new PropReceivedCommand();

        await command.ExecuteAsync(received);

        Assert.Single(
            received.Output,
            output => output is not null && output.StartsWith(_urlPrefix + "birds/"));
    }

    [Fact]
    public async Task OutputsCatUrl()
    {
        var command = new RandomCatCommand();
        var received = new PropReceivedCommand();

        await command.ExecuteAsync(received);

        Assert.Single(
            received.Output,
            output => output is not null && output.StartsWith(_urlPrefix + "cats/"));
    }
}
