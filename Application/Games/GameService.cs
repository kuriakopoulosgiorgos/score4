using GrainInterfaces.Game;

namespace Application.Games;

public class GameService(IClusterClient client)
{
    public async Task<Guid> CreateGameSession(Guid playerId)
    {
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(playerId);
        Guid guid = Guid.NewGuid();
        IGameSessionGrain gameSessionGrain = client.GetGrain<IGameSessionGrain>(guid);
        await playerGrain.Join(gameSessionGrain);
        return guid;
    }
}