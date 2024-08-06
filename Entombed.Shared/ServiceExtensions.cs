using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using Shared.Extensions;
using Shared.Input;
using Shared.Metrics;

namespace Entombed;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddInputHandling();
        services.AddMetricsHandling();
        
        services.RegisterSelfAndInterfaces<ContentLoader>();

        services.RegisterContentLookup<SpriteFont>();
        services.AddSingleton<GameInputScheme>();

        return services;
    }
}