using Discord.WebSocket;

namespace Cowboy;

internal interface ISlashCommand
{
    string Name { get; }

    string Description { get; }

    Task ExecuteAsync(SocketSlashCommand command);
}
