using GrainInterfaces.Games;
using Microsoft.AspNetCore.SignalR;
using Orleans.Streams;

namespace score4.Games;

public class GameStreamObserver(IHubContext<GameHub, IGameClient> hubContext) : IAsyncObserver<GameUpdateDto>
{
    public Task OnNextAsync(GameUpdateDto gameUpdate, StreamSequenceToken? token = null)
    {
        hubContext.Clients.Group(gameUpdate.RoomName).OnGameUpdated(gameUpdate);
        return Task.CompletedTask;
    }

    public Task OnErrorAsync(Exception ex)
    {
        return Task.CompletedTask;
    }
}