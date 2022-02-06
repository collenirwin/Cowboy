namespace Cowboy.Commands;

/// <summary>
/// Gets a random cat image url from the shibe.online api.
/// </summary>
internal class RandomCatCommand : ShibeCommandBase
{
    /// <inheritdoc />
    public override string Name => "random-cat";

    /// <inheritdoc />
    public override string Description => "Gets a link to a random cat picture";

    /// <inheritdoc />
    protected override string Endpoint => "cats";
}
