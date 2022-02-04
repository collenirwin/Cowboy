using Cowboy.Commands;
using System.Threading.Tasks;
using Xunit;

namespace Cowboy.UnitTests;

public class FlipCoin
{
    [Fact]
    public async Task OutputsHeadsOrTails()
    {
        var command = new FlipCoinCommand();
        var received = new PropReceivedCommand();

        await command.ExecuteAsync(received);

        Assert.Single(
            received.Output,
            output => output is not null &&
                (output.ToLower().Contains("heads") || output.ToLower().Contains("tails")));
    }
}
