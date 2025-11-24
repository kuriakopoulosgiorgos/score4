using GrainInterfaces.Games;

namespace score4.Games;

public interface IGameClient
{
    Task OnGameUpdated(GameUpdateDto gameUpdate);
    Task OnGameException(string message);
}