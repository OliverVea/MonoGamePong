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
        services.RegisterContentLookup<CharacterSprite>();
        services.RegisterContentLookup<TextureAtlas>();
        services.RegisterContentLookup<Effect>();

        services.RegisterInterfaces<CharacterAtlasLoader>();
        services.RegisterInterfaces<SpriteEffect1Loader>();
        services.RegisterInterfaces<PlayerCharacterSpriteLoader>();

        services.AddMouseHandling();

        services.RegisterInterfaces<GameDrawerService>();

        return services;
    }
}