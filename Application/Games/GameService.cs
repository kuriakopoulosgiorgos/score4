using GrainInterfaces.Configuration;
using GrainInterfaces.Games;

namespace Application.Games;

public class GameService(
    IClusterClient client,
    GameStreamObserver gameStreamObserver
)
{
    public async Task<Guid> CreateGameSession(Guid playerId)
    {
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(playerId);
        Guid guid = Guid.NewGuid();
        IGameGrain gameGrainGrain = client.GetGrain<IGameGrain>(guid);
        await playerGrain.Join(gameGrainGrain);
        return guid;
    }
    
    public async Task Subscribe(Guid gameId, CancellationToken cancellationToken)
    {
        var streamProvider = client.GetStreamProvider(Streams.GameStream);
        var streamId = StreamId.Create("GAMES", gameId);
        var stream = streamProvider.GetStream<GameUpdateDto>(streamId);
        await stream.SubscribeAsync(gameStreamObserver);
    }
}