using Microsoft.Extensions.DependencyInjection;

namespace Shared.Events;

public static class ServiceExtensions
{
    public static IServiceCollection AddEvent<T>(this IServiceCollection services)
    {
        services.AddSingleton<BaseEvent<T>>();
        services.AddTransient<IEventObserver<T>, EventObserver<T>>();
        services.AddTransient<IEventInvoker<T>, EventInvoker<T>>();
        
        return services;
    }
}