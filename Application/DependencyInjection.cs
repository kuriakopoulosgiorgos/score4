using Application.Games;
using GrainInterfaces.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GameService>();
        services.AddScoped<PlayerService>();
        services.AddSingleton<GameStreamObserver>();

        return services;
    }
}
