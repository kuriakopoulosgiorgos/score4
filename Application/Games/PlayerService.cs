using GrainInterfaces.Games;

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
        IGameGrain gameGrainGrain = client.GetGrain<IGameGrain>(gameSessionId);
        await playerGrain.Join(gameGrainGrain);
    }

    public async Task<int> Play(Guid playerId, int column)
    {
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(playerId);
        return await playerGrain.Play(column);
    }
}