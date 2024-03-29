﻿using Cowboy.Commands.Received;

namespace Cowboy.Commands;

/// <summary>
/// A slash command with built in rate-limiting.
/// </summary>
internal abstract class RateLimitedSlashCommand : SlashCommandBase
{
    private DateTime? _lastExecuted;

    protected const string _rateLimitErrorMessage = "The rate limit has been exceeded. " +
        "Please wait a moment before running the command again.";

    /// <summary>
    /// Time that must elapse in between <see cref="ExecuteAsync(SocketSlashCommand)"/> calls.
    /// </summary>
    protected abstract TimeSpan TimeBetweenExecutions { get; }

    /// <summary>
    /// Throws a <see cref="RateLimitExceededException"/> if <see cref="TimeBetweenExecutions"/>
    /// has not elapsed since the last call.
    /// </summary>
    /// <param name="command">The command received from Discord (includes arguments if any).</param>
    /// <exception cref="RateLimitExceededException">Thrown when not enough time has passed between calls.</exception>
    public override Task ExecuteAsync(IReceivedCommand command)
    {
        if (_lastExecuted is not null && DateTime.UtcNow.Subtract(TimeBetweenExecutions) <= _lastExecuted)
        {
            throw new RateLimitExceededException();
        }

        _lastExecuted = DateTime.UtcNow;
        return Task.CompletedTask;
    }
}
