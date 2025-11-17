using Domain.Games;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace score4;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception occured");
        httpContext.Response.StatusCode = exception switch
        {
            GameException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError,
        };

        await httpContext.Response.WriteAsJsonAsync(
            new ProblemDetails()
            {
                Type = exception.GetType().Name,
                Title = "API Error",
                Detail = exception.Message,
            },
            cancellationToken
        );
        
        return true;
    }
}