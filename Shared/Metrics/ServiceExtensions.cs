using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;

namespace Shared.Metrics;

public static class ServiceExtensions
{
    public static IServiceCollection AddMetricsHandling(this IServiceCollection services)
    {
        services.AddSingleton<TimeMetrics>();
        services.RegisterSelfAndInterfaces<TimeMetricInputService>();
        
        return services;
    }
}