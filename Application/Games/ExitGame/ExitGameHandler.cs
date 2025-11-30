using GrainInterfaces.Games;

namespace Application.Games.ExitGame;

public class ExitGameHandler(IClusterClient client)
{
    public async Task<ExitGameResponse> Handle(ExitGameRequest request)
    {
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(request.PlayerId);
        await playerGrain.Exit();
        return new ExitGameResponse();
    }
}
