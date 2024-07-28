using Entombed.Code.Game.Camera;
using Entombed.Code.Game.Levels;
using Entombed.Code.Game.Characters;
using Entombed.Code.Game.Characters.Enemies;
using Entombed.Code.Game.Characters.Players;
using Entombed.Code.Game.Gui;
using Entombed.Code.Game.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Camera;
using Shared.Extensions;
using Shared.Navigation;
using Shared.Scenes;
using Shared.Screen;
using CameraUpdateService = Entombed.Code.Game.Camera.CameraUpdateService;

namespace Entombed.Code.Game;

public class GameScene : Scene
{
    public override void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScreenHandling();
        serviceCollection.AddNavigationHandling();
        serviceCollection.AddIsometricCameraHandling();
        
        
        serviceCollection.RegisterService<GameDrawService>();
        serviceCollection.RegisterService<GameGuiService>();
        
        // Camera
        serviceCollection.AddScoped<CameraInput>();
        serviceCollection.RegisterService<CameraInputService>();
        serviceCollection.RegisterService<CameraUpdateService>();
        
        // Characters
        serviceCollection.AddSingleton<CharacterLookup>();
        serviceCollection.AddTransient<EnemySpawningService>();
        serviceCollection.AddSingleton<Player>();
        serviceCollection.AddScoped<PlayerInput>();
        serviceCollection.AddSingleton<PlayerInputScheme>();
        serviceCollection.RegisterService<PlayerInputService>();
        serviceCollection.RegisterService<PlayerUpdateService>();
        serviceCollection.RegisterSelfAndInterfaces<CharacterDrawService>();

        // Gui
        serviceCollection.RegisterInterfaces<GuiInputService>();
        serviceCollection.AddSingleton<GuiState>();
        serviceCollection.RegisterService<MetricsGuiService>();
        
        // Levels
        serviceCollection.AddTransient<DoorService>();
        serviceCollection.RegisterService<LevelCollisionService>();
        serviceCollection.AddSingleton<Level>(_ => Levels.Levels.Level1);
        serviceCollection.RegisterSelfAndInterfaces<LevelDrawService>();
        
        // Navigation
        serviceCollection.AddSingleton<NavigationState>();
        serviceCollection.RegisterService<NavigationStartupService>();
        serviceCollection.RegisterSelfAndInterfaces<NavigationGuiService>();
    }
}