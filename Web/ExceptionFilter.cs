using Domain.Games;
using Microsoft.AspNetCore.SignalR;
using score4.Games;

namespace score4;

public class ExceptionFilter : IHubFilter
{

    public async ValueTask<object?> InvokeMethodAsync(
        HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        var hub = invocationContext.Hub as GameHub;
        try
        {
            return await next(invocationContext);
        }
        catch (Exception ex)
        {
            if (ex is GameException && hub is not null)
            {
                await hub.Clients.Caller.OnGameException(ex.Message);
            }
            throw;
        }
    }
}