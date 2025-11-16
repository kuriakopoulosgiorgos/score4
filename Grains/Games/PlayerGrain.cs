using Domain.Games;
using GrainInterfaces;
using GrainInterfaces.Games;

namespace Grains.Games;

public class PlayerGrain : Grain, IPlayerGrain
{
    private PlayerDto _playerDto;
    private IGameGrain? _gameGrain;

    public PlayerGrain()
    {
        _playerDto = new PlayerDto(Id: this.GetPrimaryKeyString(), "Player");
    }
    
    public Task SetName(string name)
    {
        _playerDto = _playerDto with { Name = name };
        return Task.CompletedTask;
    }

    public Task Join(IGameGrain gameGrainGrain)
    {
        _gameGrain = gameGrainGrain;
        _gameGrain.Join(_playerDto);
        return Task.CompletedTask;
    }

    public async Task<int> Play(int column)
    {
        if (_gameGrain is null)
        {
            throw new GameException("Please join a game session before playing.");
        }
        return await _gameGrain.PlaceCell(_playerDto, column);
    }
}