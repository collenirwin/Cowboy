using Cowboy.Commands.Received;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cowboy.Commands;

/// <summary>
/// Slash command to create a random Google Maps Street View url.
/// </summary>
internal class RandomStreetViewCommand : SlashCommandBase
{
    private const string _urlPrefix = "https://www.google.com/maps/@?api=1&map_action=pano";
    private const string _coordinateFileName = "coords.json";

    private record CoordinatePair
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; init; }

        [JsonPropertyName("lng")]
        public double Longitude { get; init; }
    }

    private IList<CoordinatePair>? _coordinates;

    /// <inheritdoc />
    public override string Name => "random-street-view";

    /// <inheritdoc />
    public override string Description => "Generate a random Street View url";

    /// <inheritdoc />
    public override async Task ExecuteAsync(IReceivedCommand command)
    {
        _coordinates ??= GetAllCoordinates();
        var pair = _coordinates[Random.Shared.Next(_coordinates.Count)];

        await command.RespondAsync(text: $"{_urlPrefix}&viewpoint={pair.Latitude},{pair.Longitude}");
    }

    private static IList<CoordinatePair> GetAllCoordinates()
    {
        var json = File.ReadAllText(_coordinateFileName);

        return JsonSerializer.Deserialize<IList<CoordinatePair>>(json)
            ?? throw new Exception($"Invalid coordinates in {_coordinateFileName}.");
    }
}
