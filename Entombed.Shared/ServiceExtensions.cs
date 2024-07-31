using Entombed.Code;
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
        
        services.RegisterInterfaces<ContentLoader>();

        services.RegisterContentLookup<SpriteFont>();

        return services;
    }
}