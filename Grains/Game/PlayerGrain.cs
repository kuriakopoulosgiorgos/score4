using Domain.Game;
using GrainInterfaces.Game;

namespace Grains.Game;

public class PlayerGrain : Grain, IPlayerGrain
{
    private Player _player;
    private IGameSessionGrain? _gameSessionGrain;

    public PlayerGrain()
    {
        _player = new Player(Id: this.GetPrimaryKeyString(), "Player");
    }
    
    public Task SetName(string name)
    {
        _player = _player with { Name = name };
        return Task.CompletedTask;
    }

    public Task Join(IGameSessionGrain gameSessionGrain)
    {
        _gameSessionGrain = gameSessionGrain;
        _gameSessionGrain.Join(_player);
        return Task.CompletedTask;
    }

    public async Task<BoardStatus> Play(int column)
    {
        if (_gameSessionGrain is null)
        {
            throw new GameException("Please join a game session before playing.");
        }
        Player? playerPlaying = await _gameSessionGrain.GetPlayerPlaying();
        if (playerPlaying?.Id != _player.Id)
        {
            throw new GameException("Player is not currently playing");
        }
        return await _gameSessionGrain.PlaceCell(column);
    }
}