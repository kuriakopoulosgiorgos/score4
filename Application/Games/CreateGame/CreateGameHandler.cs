using GrainInterfaces.Games;

namespace Application.Games.CreateGame;

public class CreateGameHandler(IClusterClient client)
{
    public async Task<CreateGameResponse> Handle(CreateGameRequest request)
    {
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(request.PlayerId);
        IGameGrain gameGrainGrain = client.GetGrain<IGameGrain>(request.RoomName);
        await gameGrainGrain.Open();
        await playerGrain.Join(gameGrainGrain);
        return new CreateGameResponse(request.RoomName);
    }
}