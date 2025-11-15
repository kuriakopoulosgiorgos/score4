using Domain.Game;
using GrainInterfaces.Game;

namespace Application.Games;

public class PlayerService(IClusterClient client)
{
    
    public async Task<Guid> CreatePlayer(string name)
    {
        Guid guid = Guid.NewGuid();
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(guid);
        await playerGrain.SetName(name);
        return guid;
    }
    
    public async Task JoinGameSession(Guid playerId, Guid gameSessionId)
    {
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(playerId);
        IGameSessionGrain gameSessionGrain = client.GetGrain<IGameSessionGrain>(gameSessionId);
        await playerGrain.Join(gameSessionGrain);
    }

    public async Task<BoardStatus> Play(Guid playerId, int column)
    {
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(playerId);
        return await playerGrain.Play(column);
    }
}