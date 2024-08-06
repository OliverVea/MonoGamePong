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

        services.RegisterSelfAndInterfaces<ContentLoader>();
        
        services.RegisterContentLookup<Texture2D>();
        services.RegisterContentLookup<SoundEffect>();
        services.RegisterContentLookup<SpriteFont>();
        services.RegisterContentLookup<Effect>();

        services.RegisterSelfAndInterfaces<GameDrawerService>();
        services.RegisterSelfAndInterfaces<GameUpdateService>();
        services.RegisterSelfAndInterfaces<GameInputService>();
        services.RegisterSelfAndInterfaces<SoundService>();
        services.RegisterSelfAndInterfaces<GuiService>();

        services.AddEvent<BallHitObjectEvent>();

        return services;
    }
}