using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;

namespace Shared.Camera;

public static class ServiceExtensions
{
    public static IServiceCollection AddIsometricCamera(this IServiceCollection services, Action<CameraConfiguration>? configurationAction = null)
    {
        services.AddSingleton<CameraInput>();
        services.AddSingleton<Camera>();
        services.RegisterService<CameraUpdateService>();

        var configuration = new CameraConfiguration();

        configurationAction?.Invoke(configuration);

        services.AddSingleton(configuration);
        
        return services;
    }
}