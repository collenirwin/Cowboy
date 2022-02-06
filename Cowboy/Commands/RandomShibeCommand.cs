namespace Cowboy.Commands;

/// <summary>
/// Gets a random shibe image url from the shibe.online api.
/// </summary>
internal class RandomShibeCommand : ShibeCommandBase
{
    /// <inheritdoc />
    public override string Name => "random-shibe";

    /// <inheritdoc />
    public override string Description => "Gets a link to a random shibe picture";

    /// <inheritdoc />
    protected override string Endpoint => "shibes";
}
