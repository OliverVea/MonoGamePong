using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using ShaderSandbox.Game;
using Shared.Content;
using Shared.Extensions;
using Shared.Lifetime;
using Shared.Mouse;
using Shared.Sprites;

namespace ShaderSandbox;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddMouseHandling();
        services.AddGameServices();
        
        return services;
    }
    
    private static void AddGameServices(this IServiceCollection services)
    {
        services.RegisterContentLookup<CharacterSprite>()
            .RegisterContentLookup<TextureAtlas>()
            .RegisterContentLookup<Effect>();

        services.RegisterInterfaces<CharacterAtlasLoader>()
            .RegisterInterfaces<SpriteEffect1Loader>()
            .RegisterInterfaces<PlayerCharacterSpriteLoader>();

        services.RegisterService<GameDrawerService>();
    }
}