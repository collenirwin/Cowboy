using Cowboy.Commands;
using System.Threading.Tasks;
using Xunit;

namespace Cowboy.UnitTests;

public class RandomWikiPage
{
    private const string _wikiPageUrlPrefix = "https://en.wikipedia.org/wiki/";

    [Fact]
    public async Task OutputsWikipediaUrl()
    {
        var command = new RandomWikiPageCommand();
        var received = new PropReceivedCommand();

        await command.ExecuteAsync(received);

        Assert.Single(
            received.Output,
            output => output is not null &&
                output.StartsWith(_wikiPageUrlPrefix) &&
                output.Length > _wikiPageUrlPrefix.Length);
    }
}
