using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using ShaderSandbox.Game;
using Shared.Content;
using Shared.Extensions;
using Shared.Input;
using Shared.Input.Mouse;
using Shared.Lifetime;
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

        services.RegisterSelfAndInterfaces<CharacterAtlasLoader>()
            .RegisterSelfAndInterfaces<SpriteEffect1Loader>()
            .RegisterSelfAndInterfaces<PlayerCharacterSpriteLoader>()
            .RegisterSelfAndInterfaces<GameDrawerService>();
    }
}