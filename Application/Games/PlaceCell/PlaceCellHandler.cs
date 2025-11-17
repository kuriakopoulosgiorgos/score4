using GrainInterfaces.Games;

namespace Application.Games.PlaceCell;

public class PlaceCellHandler(IClusterClient client)
{
    public async Task<PlaceCellResponse> Handle(PlaceCellRequest request)
    {
        IPlayerGrain playerGrain = client.GetGrain<IPlayerGrain>(request.PlayerId);
        return new PlaceCellResponse(await playerGrain.PlaceCell(request.Column));
    }
}