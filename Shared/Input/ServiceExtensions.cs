using Microsoft.Extensions.DependencyInjection;
using Shared.Input.Keyboard;
using Shared.Input.Mouse;
using Shared.Lifetime;

namespace Shared.Input;

public static class ServiceExtensions
{
    public static IServiceCollection AddInputHandling(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMouseHandling();
        serviceCollection.AddKeyboardHandling();
        
        return serviceCollection;
    }
    
    public static IServiceCollection AddMouseHandling(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IInputService, MouseInputService>();
        serviceCollection.AddSingleton<Mouse.Mouse>();
        
        return serviceCollection;
    }
    
    public static IServiceCollection AddKeyboardHandling(this IServiceCollection serviceCollection)
    {
        var keyboard = new Keyboard.Keyboard();
        
        serviceCollection.AddSingleton<IInputService, KeyboardInputService>();
        serviceCollection.AddSingleton(keyboard);
        
        return serviceCollection;
    }
}