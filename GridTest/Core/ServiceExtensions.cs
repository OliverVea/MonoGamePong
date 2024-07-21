using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using ShaderSandbox.Core.Models;
using ShaderSandbox.Core.Services;

namespace ShaderSandbox.Core;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<GameState>();
        
        services.AddContentLookup<CharacterSprite>();
        services.AddContentLookup<TextureAtlas>();
        services.AddContentLookup<Effect>();

        services.AddScoped<IGameDrawerService<GameInput>, GameDrawerService>();
        services.AddScoped<IGameLogicService<GameInput>, GameLogicService>();
        services.AddScoped<IGameInputService<GameInput>, GameInputService>();

        return services;
    }

    private static void AddContentLookup<T>(this IServiceCollection services)
    {
        services.AddSingleton<ContentLookup<T>>();
        services.AddTransient<Initializable>(sp => sp.GetRequiredService<ContentLookup<T>>());
    }
}