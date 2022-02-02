namespace Cowboy.Utils;

/// <summary>
/// Contains a single <see cref="HttpClient"/> instance.
/// </summary>
internal static class Http
{
    /// <summary>
    /// Global <see cref="HttpClient"/> instance.
    /// </summary>
    public static HttpClient Client { get; } = new HttpClient();
}
