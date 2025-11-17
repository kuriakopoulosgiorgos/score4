using Application.Games.CreatePlayer;
using Application.Games.JoinGame;
using Application.Games.PlaceCell;
using Microsoft.AspNetCore.Mvc;

namespace score4.Games;

[ApiController]
[Route("/api/v1/players")]
public class PlayerController : ControllerBase
{
  
    [HttpPost]
    public async Task<CreatePlayerResponse> CreatePlayer(
        [FromServices] CreatePlayerHandler handler,
        [FromBody] CreatePlayerRequest request)
    {
        return await handler.Handle(request);
    }
    
    [HttpPost]
    [Route("joinGame")]
    public async Task<JoinGameResponse> JoinGame(
        [FromServices] JoinGameHandler handler,
        [FromBody] JoinGameRequest request)
    {
        return await handler.Handle(request);
    }
    
    [HttpPost]
    [Route("placeCell")]
    public async Task<PlaceCellResponse> Play(
        [FromServices] PlaceCellHandler handler,
        [FromBody] PlaceCellRequest request)
    {
        return await handler.Handle(request);
    }
}