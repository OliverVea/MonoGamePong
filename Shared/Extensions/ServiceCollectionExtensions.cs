using Microsoft.Extensions.DependencyInjection;
using Shared.Content;
using Shared.Lifetime;

namespace Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterSelfAndInterfaces<T>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(T), typeof(T), serviceLifetime);
        services.Add(serviceDescriptor);
        services.RegisterInterfaces<T>(serviceLifetime);
        
        return services;
    }
    
    public static IServiceCollection RegisterInterfaces<T>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class
    {
        var serviceType = typeof(T);
        var interfaces = serviceType.GetInterfaces();
        
        foreach (var interfaceType in interfaces)
        {
            var serviceDescriptor = new ServiceDescriptor(interfaceType, serviceType, serviceLifetime);
            services.Add(serviceDescriptor);
        }
        
        return services;
    }
    
    public static IServiceCollection RegisterService<T>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class
    {
        var serviceType = typeof(T);
        
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

        if (StartupServiceType.IsAssignableFrom(serviceType))
        {
            services.AddTransient<IStartupService>(sp => (IStartupService)sp.GetRequiredService<T>());
        }
        
        if (UpdateServiceType.IsAssignableFrom(serviceType))
        {
            services.AddTransient<IUpdateService>(sp => (IUpdateService)sp.GetRequiredService<T>());
        }
        
        if (DrawServiceType.IsAssignableFrom(serviceType))
        {
            services.AddTransient<IDrawService>(sp => (IDrawService)sp.GetRequiredService<T>());
        }
        
        if (InputServiceType.IsAssignableFrom(serviceType))
        {
            services.AddTransient<IInputService>(sp => (IInputService)sp.GetRequiredService<T>());
        }
        
        if (GuiServiceType.IsAssignableFrom(serviceType))
        {
            services.AddTransient<IGuiService>(sp => (IGuiService)sp.GetRequiredService<T>());
        }
        
        return services;
    }
    
    public static IServiceCollection RegisterContentLookup<T>(this IServiceCollection services)
    {
        services.AddSingleton<ContentLookup<T>>();
        services.AddTransient<IStartupService>(sp => sp.GetRequiredService<ContentLookup<T>>());
        
        return services;
    }
    
    private static readonly Type StartupServiceType = typeof(IStartupService);
    private static readonly Type UpdateServiceType = typeof(IUpdateService);
    private static readonly Type DrawServiceType = typeof(IDrawService);
    private static readonly Type InputServiceType = typeof(IInputService);
    private static readonly Type GuiServiceType = typeof(IGuiService);
}