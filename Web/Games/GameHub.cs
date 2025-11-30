using Application.Games.CreateGame;
using Application.Games.CreatePlayer;
using Application.Games.ExitGame;
using Application.Games.JoinGame;
using Application.Games.PlaceCell;
using Application.Games.PlayAgain;
using GrainInterfaces.Games;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace score4.Games;

public class GameHub(ExitGameHandler exitGameHandler) : Hub<IGameClient>
{
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
        await exitGameHandler.Handle(new ExitGameRequest(Context.ConnectionId));
    }

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
    
    public async Task PlaceCell(
        [FromServices] PlaceCellHandler handler,
        int column)
    {
        await handler.Handle(new PlaceCellRequest(Context.ConnectionId, column));
    }
    
    public async Task PlayAgain(
        [FromServices] PlayAgainHandler handler,
        string roomName)
    {
        await handler.Handle(new PlayAgainRequest(roomName));
    }
    
    public async Task Exit(
        [FromServices] ExitGameHandler handler)
    {
        await handler.Handle(new ExitGameRequest(Context.ConnectionId));
    }
}