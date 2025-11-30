using GrainInterfaces.Games;

namespace Application.Games.PlayAgain;

public class PlayAgainHandler(IClusterClient client)
{
    public async Task Handle(PlayAgainRequest request)
    {
        IGameGrain gameGrainGrain = client.GetGrain<IGameGrain>(request.RoomName);
        await gameGrainGrain.PlayAgain();
    }
}