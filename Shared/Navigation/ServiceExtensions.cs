using Microsoft.Extensions.DependencyInjection;

namespace Shared.Navigation;

public static class ServiceExtensions
{
    public static IServiceCollection AddNavigationHandling(this IServiceCollection services)
    {
        services.AddTransient<PathfindingService>();
        services.AddTransient<NavigationGraphService>();
        
        return services;
    }
}