using Entombed.Game.Levels;
using Entombed.Game.Levels.Generation;
using Entombed.Game.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;
using Shared.Navigation;
using Shared.Scenes;

namespace Entombed.Loading;

public class LoadingScene : Scene
{
    public override void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddNavigationHandling();
        
        serviceCollection.AddSingleton<LoadingState>();
        
        serviceCollection.RegisterSelfAndInterfaces<LoadingUpdateService>();
        serviceCollection.RegisterSelfAndInterfaces<LoadingDrawService>();
        serviceCollection.RegisterSelfAndInterfaces<LevelGeneratorService>();
        serviceCollection.RegisterSelfAndInterfaces<NavigationStateService>();
        serviceCollection.RegisterSelfAndInterfaces<LevelGeometryService>();
    }
}