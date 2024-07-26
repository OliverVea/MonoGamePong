using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;

namespace Shared.Camera;

public static class ServiceExtensions
{
    public static IServiceCollection AddIsometricCameraHandling(this IServiceCollection services, Action<IsometricCameraConfiguration>? configurationAction = null)
    {
        services.AddSingleton<IsometricCamera>();
        
        var configuration = new IsometricCameraConfiguration();

        configurationAction?.Invoke(configuration);

        services.AddSingleton(configuration);
        
        return services;
    }
}