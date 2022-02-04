using Cowboy.Commands;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cowboy.UnitTests;

public class Help
{
    [Fact]
    public async Task ContainsAllCommandsAndDescriptions()
    {
        var command = new HelpCommand();
        var received = new PropReceivedCommand();

        await command.ExecuteAsync(received);

        Assert.Single(
            received.Output,
            output => output is not null &&
                SlashCommands.All.Values.All(cmd => output.Contains(cmd.Name) && output.Contains(cmd.Description)));
    }
}
