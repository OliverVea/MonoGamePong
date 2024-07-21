using Microsoft.Extensions.DependencyInjection;
using Shared.Content;
using Shared.Lifetime;

namespace Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterThisAndInterfaces<T>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(T), typeof(T), serviceLifetime);
        services.Add(serviceDescriptor);
        services.RegisterInterfaces<T>(serviceLifetime);
    }
    
    public static void RegisterInterfaces<T>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class
    {
        var serviceType = typeof(T);
        var interfaces = serviceType.GetInterfaces();
        
        foreach (var interfaceType in interfaces)
        {
            var serviceDescriptor = new ServiceDescriptor(interfaceType, serviceType, serviceLifetime);
            services.Add(serviceDescriptor);
        }
    }
    
    public static void RegisterService<T>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(T), typeof(T), serviceLifetime);
        services.Add(serviceDescriptor);
        
        foreach (var serviceType in ServiceTypes)
        {
            if (!serviceType.IsAssignableFrom(typeof(T))) continue;
            
            var interfaceDescriptor = new ServiceDescriptor(serviceType, sp => sp.GetRequiredService<T>(), serviceLifetime);
            services.Add(interfaceDescriptor);
        }
    }
    
    public static void RegisterContentLookup<T>(this IServiceCollection services)
    {
        services.AddSingleton<ContentLookup<T>>();
        services.AddTransient<IStartupService>(sp => sp.GetRequiredService<ContentLookup<T>>());
    }
    
    
    private static readonly Type StartupServiceType = typeof(IStartupService);
    private static readonly Type UpdateServiceType = typeof(IUpdateService);
    private static readonly Type DrawServiceType = typeof(IDrawService);
    private static readonly Type InputServiceType = typeof(IInputService);
    
    private static readonly Type[] ServiceTypes = [
        StartupServiceType,
        UpdateServiceType,
        DrawServiceType,
        InputServiceType
    ];
}