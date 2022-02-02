using Cowboy.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Cowboy.UnitTests;

public class RateLimits
{
    private const int _msBetweenExecutions = 100;

    private class FakeRateLimitedCommand : RateLimitedSlashCommand
    {
        public override string Name => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        protected override TimeSpan TimeBetweenExecutions => TimeSpan.FromMilliseconds(_msBetweenExecutions);

        public override Task ExecuteAsync(SocketSlashCommand command) => base.ExecuteAsync(command);
    }

    [Fact]
    public async Task ShouldThrowForViolatingRateLimit()
    {
        var command = new FakeRateLimitedCommand();

        await command.ExecuteAsync(null!);

        await Assert.ThrowsAsync<RateLimitExceededException>(() => command.ExecuteAsync(null!));
    }

    [Fact]
    public async Task ShouldNotThrowWhenObeyingRateLimit()
    {
        var command = new FakeRateLimitedCommand();

        await command.ExecuteAsync(null!);

        await Task.Delay(_msBetweenExecutions);

        await command.ExecuteAsync(null!);
    }
}
