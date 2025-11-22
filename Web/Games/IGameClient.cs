using GrainInterfaces.Games;

namespace score4.Games;

public interface IGameClient
{
    Task OnGameUpdate(GameUpdateDto gameUpdate);
    Task OnGameException(string message);
}