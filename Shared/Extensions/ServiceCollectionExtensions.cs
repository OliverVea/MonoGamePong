using Microsoft.Extensions.DependencyInjection;
using Shared.Content;
using Shared.Lifetime;

namespace Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterSelfAndInterfaces<T>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Transient) where T : class
    {
        switch (serviceLifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton<T>();
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped<T>();
                break;
            case ServiceLifetime.Transient:
                services.AddTransient<T>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null);
        }
        
        var serviceType = typeof(T);
        var interfaces = serviceType.GetInterfaces();

        foreach (var interfaceType in interfaces)
        {
            var serviceDescriptor = new ServiceDescriptor(interfaceType, sp => sp.GetRequiredService<T>(), serviceLifetime);
            services.Add(serviceDescriptor);
        }
        
        return services;
    }
    
    public static IServiceCollection RegisterContentLookup<T>(this IServiceCollection services)
    {
        services.AddSingleton<ContentLookup<T>>();
        services.AddTransient<IStartupService>(sp => sp.GetRequiredService<ContentLookup<T>>());
        
        return services;
    }
}