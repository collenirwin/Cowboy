using Cowboy.Commands;
using System.IO;
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

        try
        {
            await command.ExecuteAsync(received);
        }
        catch (FileNotFoundException)
        {
            // don't run this test via CI because the file won't be there
            return;
        }

        Assert.Single(
            received.Output,
            output => output is not null &&
                output.StartsWith(_urlPrefix) &&
                Regex.IsMatch(output, @"&viewpoint=-?\d{1,2}\.\d+,-?1?\d{1,2}\.\d+$"));
    }
}
