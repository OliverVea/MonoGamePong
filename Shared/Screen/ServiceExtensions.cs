using Microsoft.Extensions.DependencyInjection;
using Shared.Lifetime;

namespace Shared.Screen;

public static class ServiceExtensions
{
    public static IServiceCollection AddScreenHandling(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IInputService, ScreenService>();
        serviceCollection.AddSingleton<IStartupService, ScreenService>();
        
        serviceCollection.AddSingleton<Screen>();
        
        return serviceCollection;
    }
}