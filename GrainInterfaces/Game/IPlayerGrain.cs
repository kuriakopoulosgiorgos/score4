using Domain.Game;

namespace GrainInterfaces.Game;

public interface IPlayerGrain : IGrainWithGuidKey
{
    Task SetName(string name);
    Task Join(IGameSessionGrain gameSession);
    Task<BoardStatus> Play(int column);
}