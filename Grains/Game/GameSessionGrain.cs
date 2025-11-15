using Domain.Game;
using GrainInterfaces.Game;

namespace Grains.Game;

public class GameSessionGrain : Grain, IGameSessionGrain
{
    private readonly GameSession _gameSession;

    public GameSessionGrain()
    {
        _gameSession = new GameSession()
        {
            SessionId = this.GetPrimaryKeyString(),
        };
    }
    
    public Task Join(Player player)
    {
        _gameSession.Join(player);
        return Task.CompletedTask;
    }

    public Task<Player?> GetPlayerPlaying()
    {
        return Task.FromResult(_gameSession.GetPlayerPlaying());
    }

    public Task<BoardStatus> PlaceCell(int column)
    {
        return Task.FromResult(_gameSession.PlaceCell(column));
    }
}