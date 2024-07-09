using Microsoft.Extensions.DependencyInjection;
using Pong.Core.Models;
using Pong.Core.Services;
using Pong.Core.Textures;

namespace Pong.Core;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<GameState>();
        services.AddSingleton<TextureLookup>();

        services.AddTransient<ITextureLoader, BallTextureLoader>();
        services.AddTransient<ITextureLoader, PaddleTextureLoader>();

        services.AddScoped<GameDrawerService>();
        services.AddScoped<GameLogicService>();
        services.AddScoped<GameInputService>();

        services.AddTransient<Initializable>(sp => sp.GetRequiredService<TextureLookup>());

        return services;
    }
}