using Application.Games;
using Application.Games.CreateGame;
using Application.Games.CreatePlayer;
using Application.Games.JoinGame;
using Application.Games.PlaceCell;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateGameHandler>();
        services.AddScoped<CreatePlayerHandler>();
        services.AddScoped<JoinGameHandler>();
        services.AddScoped<PlaceCellHandler>();
        services.AddSingleton<GameStreamObserver>();
        services.AddHostedService<GameStreamSubscriberHostService>();
        return services;
    }
}
