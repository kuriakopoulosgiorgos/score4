using Domain.Games;
using GrainInterfaces.Configuration;
using GrainInterfaces.Games;

namespace Grains.Games;

public class GameGrain : Grain, IGameGrain
{
    private readonly Game _game;

    public GameGrain()
    {
        _game = new Game()
        {
            SessionId = this.GetPrimaryKeyString(),
        };

        var streamProvider = this.GetStreamProvider(Streams.GameStream);
        var streamId = StreamId.Create(Streams.GameStreamNameSpace, Streams.GameStreamId);
        var stream = streamProvider.GetStream<GameUpdateDto>(streamId);
        _game.GameUpdated += async (_, gameUpdate) =>
        {
            await stream.OnNextAsync(gameUpdate.ToGameUpdateDto());
        };
    }

    public Task Open()
    {
        _game.Open();
        return Task.CompletedTask;
    }

    public Task Join(PlayerDto playerDto)
    {
        _game.Join(new Player(playerDto.Id, playerDto.Name));
        return Task.CompletedTask;
    }

    public Task<int> PlaceCell(PlayerDto playerDto, int column)
    {
        return Task.FromResult(_game.PlaceCell(new Player(playerDto.Id, playerDto.Name), column));
    }
}