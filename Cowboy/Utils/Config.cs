using System.Text.Json;

namespace Cowboy.Utils;

/// <summary>
/// Contains properties used to configure the application.
/// </summary>
internal class Config
{
    /// <summary>
    /// Name of the config file.
    /// </summary>
    public const string FileName = "cowboy-config.json";

    /// <summary>
    /// Path to the config file directory.
    /// </summary>
    public static readonly string DirectoryPath = Path.Combine(
        Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData,
            Environment.SpecialFolderOption.DoNotVerify),
        "Cowboy");

    /// <summary>
    /// Path to the config file.
    /// </summary>
    public static readonly string FilePath = Path.Combine(DirectoryPath, FileName);

    /// <summary>
    /// Discord access token.
    /// </summary>
    public string? Token { get; init; }

    /// <summary>
    /// Loads a <see cref="Config"/> object from a json config file at <see cref="FilePath"/>,
    /// prompting for required parts to create one if not found.
    /// </summary>
    /// <returns>The parsed config.</returns>
    /// <exception cref="Exception">Thrown if no token is provided, or if the config file is incomplete.</exception>
    public static Config CreateFromFile()
    {
        Config? config;

        if (!File.Exists(FilePath))
        {
            Console.WriteLine("Config file not found.");
            Console.Write("Discord token: ");

            var token = Console.ReadLine();

            if (token is null)
            {
                throw new Exception("Token must be provided.");
            }

            config = new Config
            {
                Token = token
            };

            Directory.CreateDirectory(DirectoryPath);
            File.WriteAllText(FilePath, JsonSerializer.Serialize(config));
            return config;
        }

        config = JsonSerializer.Deserialize<Config>(File.ReadAllText(FilePath));

        if (config is null)
        {
            throw new Exception($"Config file incomplete (path: {FilePath}).");
        }

        return config;
    }
}
