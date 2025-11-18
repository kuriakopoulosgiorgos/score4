using Microsoft.AspNetCore.SignalR;

namespace score4.Games;

public class GameHub : Hub<IGameClient>
{
    public Task JoinRoom(string roomName)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    }
}