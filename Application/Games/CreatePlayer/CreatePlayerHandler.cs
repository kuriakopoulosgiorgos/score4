using GrainInterfaces.Games;

namespace Application.Games.CreatePlayer;

public class CreatePlayerHandler(IClusterClient client)
{
    public async Task<CreatePlayerResponse> Handle(CreatePlayerRequest request)
    {
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(request.PlayerId);
        await playerGrain.SetName(request.Name);
        return new CreatePlayerResponse(request.PlayerId);
    }
}