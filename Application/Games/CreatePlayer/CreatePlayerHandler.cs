using GrainInterfaces.Games;

namespace Application.Games.CreatePlayer;

public class CreatePlayerHandler(IClusterClient client)
{
    public async Task<CreatePlayerResponse> Handle(CreatePlayerRequest request)
    {
        Guid guid = Guid.NewGuid();
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(guid);
        await playerGrain.SetName(request.Name);
        return new CreatePlayerResponse(guid);
    }
}