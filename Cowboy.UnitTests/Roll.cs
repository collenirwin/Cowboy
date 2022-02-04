using Cowboy.Commands;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Cowboy.UnitTests;

public class Roll
{
    [Fact]
    public async Task WorksWithDefaults()
    {
        var command = new RollCommand();
        var received = new PropReceivedCommand();

        await command.ExecuteAsync(received);

        Assert.Single(received.Output, output => output is not null);
        Assert.InRange(GetResult(received.Output.First()!), 1, 10);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(-10000)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(int.MinValue)]
    public async Task WorksWithLowInBounds(int low)
    {
        var command = new RollCommand();
        var received = new PropReceivedCommand(new[]
        {
            new CommandOption("low", low)
        });

        await command.ExecuteAsync(received);

        Assert.Single(received.Output, output => output is not null);
        Assert.InRange(GetResult(received.Output.First()!), low, 10);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(10000)]
    [InlineData(5)]
    [InlineData(int.MaxValue - 1)] // adding 1 in method
    public async Task WorksWithHighInBounds(int high)
    {
        var command = new RollCommand();
        var received = new PropReceivedCommand(new[]
        {
            new CommandOption("high", high)
        });

        await command.ExecuteAsync(received);

        Assert.Single(received.Output, output => output is not null);
        Assert.InRange(GetResult(received.Output.First()!), 1, high);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(int.MinValue, int.MaxValue - 1)]
    public async Task WorksWithBothInBounds(int low, int high)
    {
        var command = new RollCommand();
        var received = new PropReceivedCommand(new[]
        {
            new CommandOption("low", low),
            new CommandOption("high", high)
        });

        await command.ExecuteAsync(received);

        Assert.Single(received.Output, output => output is not null);
        Assert.InRange(GetResult(received.Output.First()!), low, high);
    }

    [Fact]
    public async Task ThrowsWhenOutOfBounds()
    {
        var command = new RollCommand();
        var received = new PropReceivedCommand(new[]
        {
            new CommandOption("low", 1),
            new CommandOption("high", 0)
        });

        await Assert.ThrowsAsync<InvalidOperationException>(() => command.ExecuteAsync(received));
    }

    private static int GetResult(string output)
    {
        return int.Parse(Regex.Match(output, @"\*\*(-?\d+)\*\*$").Groups[1].Value);
    }
}
