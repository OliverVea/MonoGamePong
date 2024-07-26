using Microsoft.Extensions.DependencyInjection;

namespace DITemplate;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSystems();
        services.AddGameServices();

        return services;
    }
    
    /// <summary>
    /// Registers your game systems.
    /// </summary>
    /// <example>
    /// <code>
    /// services.AddMouseHandling();
    /// services.AddScreenHandling();
    /// services.AddIsometricCameraHandling();
    /// </code>
    /// </example>
    /// <param name="services"></param>
    private static void AddSystems(this IServiceCollection services)
    {
    }

    /// <summary>
    /// Registers your game services.
    /// See following game system interfaces:
    /// <see cref="Shared.Lifetime.IStartupService"/>,
    /// <see cref="Shared.Lifetime.IInputService"/>,
    /// <see cref="Shared.Lifetime.IUpdateService"/>,
    /// <see cref="Shared.Lifetime.IDrawService"/>
    /// </summary>
    /// <param name="services"></param>
    private static void AddGameServices(this IServiceCollection services)
    {
    }
}