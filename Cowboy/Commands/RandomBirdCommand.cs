namespace Cowboy.Commands;

/// <summary>
/// Gets a random bird image url from the shibe.online api.
/// </summary>
internal class RandomShibeCommand : ShibeCommandBase
{
    /// <inheritdoc />
    public override string Name => "random-bird";

    /// <inheritdoc />
    public override string Description => "Gets a link to a random bird picture";

    /// <inheritdoc />
    protected override string Endpoint => "birds";
}
