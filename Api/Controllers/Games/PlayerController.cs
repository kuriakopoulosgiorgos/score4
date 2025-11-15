using Application.Games;
using Domain.Game;
using Microsoft.AspNetCore.Mvc;

namespace score4.Controllers.Games;

[ApiController]
[Route("api/v1/players")]
public class PlayerController(
    PlayerService playerService
) : ControllerBase
{
  
    [HttpPost]
    [Route("/{name}")]
    public async Task<Guid> CreatePlayer([FromRoute] string name)
    {
        return await playerService.CreatePlayer(name);
    }
    
    [HttpPost]
    [Route("/{playerId:guid}/join/{gameSessionId:guid}")]
    public async Task JoinGameSession(Guid playerId, Guid gameSessionId)
    {
        await playerService.JoinGameSession(playerId, gameSessionId);
    }
    
    [HttpPost]
    [Route("/{playerId:guid}/play/{column:int}")]
    public async Task<BoardStatus> Play(Guid playerId, int column)
    {
        return await playerService.Play(playerId, column);
    }
}