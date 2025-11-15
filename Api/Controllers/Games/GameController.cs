using Application.Games;
using Microsoft.AspNetCore.Mvc;

namespace score4.Controllers.Games;

[ApiController]
[Route("api/v1/games")]
public class GameController(
    GameService gameService
) : ControllerBase
{
  
    [HttpPost]
    [Route("/{playerId:guid}")]
    public async Task<Guid> CreateGameSession([FromRoute] Guid playerId)
    {
        return await gameService.CreateGameSession(playerId);
    }
}