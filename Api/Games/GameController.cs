using Application.Games.CreateGame;
using Microsoft.AspNetCore.Mvc;

namespace score4.Games;

[ApiController]
[Route("api/v1/games")]
public class GameController : ControllerBase
{
  
    [HttpPost]
    public async Task<CreateGameResponse> CreateGame(
        [FromServices] CreateGameHandler handler,
        [FromBody] CreateGameRequest request)
    {
        return await handler.Handle(request);
    }
}