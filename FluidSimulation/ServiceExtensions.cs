using FluidSimulation.Game;
using Microsoft.Extensions.DependencyInjection;
using Shared.Lifetime;
using Shared.Mouse;
using Shared.Screen;

namespace FluidSimulation;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddMouseHandling();
        services.AddScreenHandling();

        services.RegisterService<IDrawService, ParticleDrawingService>();
        services.AddSingleton<IStartupService, ParticleStartupService>();
        services.AddSingleton<IUpdateService, ParticleUpdateService>();
        
        services.AddSingleton<Particles>();

        return services;
    }
}