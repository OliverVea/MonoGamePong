using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using Shared.Extensions;

namespace Pong.Core;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<GameInput>();
        services.AddSingleton<GameState>();
        services.AddSingleton<GameProperties>();

        services.RegisterInterfaces<BallTextureLoader>();
        services.RegisterInterfaces<PaddleTextureLoader>();
        
        services.RegisterContentLookup<Texture2D>();

        services.RegisterService<GameDrawerService>();
        services.RegisterService<GameUpdateService>();
        services.RegisterService<GameInputService>();

        return services;
    }
}