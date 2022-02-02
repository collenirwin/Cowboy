namespace Cowboy.Commands;

/// <summary>
/// Thrown when a rate limit is hit.
/// </summary>
internal class RateLimitExceededException : Exception
{
}
