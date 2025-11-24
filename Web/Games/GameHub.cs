using Application.Games.CreateGame;
using Application.Games.CreatePlayer;
using Application.Games.JoinGame;
using Application.Games.PlaceCell;
using GrainInterfaces.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace score4.Games;

public class GameHub() : Hub<IGameClient>
{
    public async Task<PlayerDto> CreatePlayer(
        [FromServices] CreatePlayerHandler handler,
        string name)
    {
        CreatePlayerResponse createPlayerResponse = await handler.Handle(new CreatePlayerRequest(Context.ConnectionId, name));
        return new PlayerDto(createPlayerResponse.PlayerId, name);
    }
    
    public async Task JoinGame(
        [FromServices] JoinGameHandler handler,
        string roomName)
    {
        await handler.Handle(new JoinGameRequest(Context.ConnectionId, roomName));
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    }
    
    public async Task CreateGame(
        [FromServices] CreateGameHandler handler,
        string roomName)
    {
        await handler.Handle(new CreateGameRequest(Context.ConnectionId, roomName));
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    }
    
    public async Task PLaceCell(
        [FromServices] PlaceCellHandler handler,
        int column)
    {
        await handler.Handle(new PlaceCellRequest(Context.ConnectionId, column));
    }
}