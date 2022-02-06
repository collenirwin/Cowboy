using Cowboy.Commands;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Cowboy.UnitTests;

public class RandomStreetView
{
    private const string _urlPrefix = "https://www.google.com/maps/@?api=1&map_action=pano";

    [Fact]
    public async Task OutputsStreetViewUrl()
    {
        var command = new RandomStreetViewCommand();
        var received = new PropReceivedCommand();

        await command.ExecuteAsync(received);

        Assert.Single(
            received.Output,
            output => output is not null &&
                output.StartsWith(_urlPrefix) &&
                Regex.IsMatch(output, @"&viewpoint=-?\d{1,2}\.\d+,-?1?\d{1,2}\.\d+$"));
    }
}
