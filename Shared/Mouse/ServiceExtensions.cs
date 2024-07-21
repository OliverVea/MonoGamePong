using Microsoft.Extensions.DependencyInjection;
using Shared.Lifetime;

namespace Shared.Mouse;

public static class ServiceExtensions
{
    public static IServiceCollection AddMouseHandling(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IInputService, MouseInputService>();
        serviceCollection.AddSingleton<Mouse>();
        
        return serviceCollection;
    }
}