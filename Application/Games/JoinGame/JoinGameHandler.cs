using GrainInterfaces.Games;

namespace Application.Games.JoinGame;

public class JoinGameHandler(IClusterClient client)
{
    public async Task<JoinGameResponse> Handle(JoinGameRequest request)
    {
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(request.PlayerId);
        IGameGrain gameGrainGrain = client.GetGrain<IGameGrain>(request.RoomName);
        await playerGrain.Join(gameGrainGrain);
        return new JoinGameResponse();
    }
}