using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using ShaderSandbox.Game;
using Shared.Content;
using Shared.Lifetime;
using Shared.Mouse;
using Shared.Sprites;

namespace ShaderSandbox;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddContentLookup<CharacterSprite>();
        services.AddContentLookup<TextureAtlas>();
        services.AddContentLookup<Effect>();

        services.AddTransient<IContentLoader<TextureAtlas>, CharacterAtlasLoader>();
        services.AddTransient<IContentLoader<Effect>, SpriteEffect1Loader>();
        services.AddTransient<IContentLoader<CharacterSprite>, PlayerCharacterSpriteLoader>();

        services.AddMouseHandling();

        services.AddSingleton<IDrawService, GameDrawerService>();

        return services;
    }

    private static void AddContentLookup<T>(this IServiceCollection services)
    {
        services.AddSingleton<ContentLookup<T>>();
        services.AddTransient<IStartupService>(sp => sp.GetRequiredService<ContentLookup<T>>());
    }
}