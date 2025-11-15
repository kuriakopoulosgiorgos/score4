using Domain.Game;

namespace GrainInterfaces.Game;

public interface IGameSessionGrain :  IGrainWithGuidKey
{
    Task Join(Player player);
    
    Task<Player?> GetPlayerPlaying();
    
    public Task<BoardStatus> PlaceCell(int column);
}