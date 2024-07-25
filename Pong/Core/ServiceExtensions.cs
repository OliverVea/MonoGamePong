using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Shared.Events;
using Shared.Extensions;
using Shared.Screen;

namespace Pong.Core;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScreenHandling();
        
        services.AddSingleton<GameInput>();
        services.AddSingleton<GameState>();
        services.AddSingleton<GameProperties>();

        services.RegisterInterfaces<ContentLoader>();
        
        services.RegisterContentLookup<Texture2D>();
        services.RegisterContentLookup<SoundEffect>();
        services.RegisterContentLookup<SpriteFont>();
        services.RegisterContentLookup<Effect>();

        services.RegisterService<GameDrawerService>();
        services.RegisterService<GameUpdateService>();
        services.RegisterService<GameInputService>();
        services.RegisterService<SoundService>();
        services.RegisterService<GuiService>();

        services.AddEvent<BallHitObjectEvent>();

        return services;
    }
}