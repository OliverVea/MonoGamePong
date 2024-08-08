using Entombed.Game.Camera;
using Entombed.Game.Characters;
using Entombed.Game.Characters.Enemies;
using Entombed.Game.Characters.Enemies.GoalBehaviors;
using Entombed.Game.Characters.Events;
using Entombed.Game.Characters.Players;
using Entombed.Game.Gui;
using Entombed.Game.Levels;
using Entombed.Game.Menu;
using Entombed.Game.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Camera;
using Shared.Extensions;
using Shared.Navigation;
using Shared.Scenes;
using Shared.Screen;
using CameraUpdateService = Entombed.Game.Camera.CameraUpdateService;

namespace Entombed.Game;

public class GameScene(Level level, Player player, NavigationState navigationState) : Scene
{
    public override void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(_ => player);
        serviceCollection.AddSingleton(_ => level);
        serviceCollection.AddSingleton(_ => navigationState);
        
        serviceCollection.AddScreenHandling();
        serviceCollection.AddNavigationHandling();
        serviceCollection.AddIsometricCameraHandling();
        
        serviceCollection.RegisterSelfAndInterfaces<GameDrawService>();
        serviceCollection.RegisterSelfAndInterfaces<GameGuiService>();
        
        serviceCollection.AddSingleton<GamePaused>();
        
        // Camera
        serviceCollection.AddScoped<CameraInput>();
        serviceCollection.RegisterSelfAndInterfaces<CameraInputService>();
        serviceCollection.RegisterSelfAndInterfaces<CameraUpdateService>();
        
        // Characters
        serviceCollection.AddScoped<PlayerInput>();
        serviceCollection.AddSingleton<CharacterLookup>();
        serviceCollection.RegisterSelfAndInterfaces<CharacterDamageService>();
        serviceCollection.RegisterSelfAndInterfaces<EnemySpawningService>();
        serviceCollection.RegisterSelfAndInterfaces<EnemyUpdateService>();
        serviceCollection.RegisterSelfAndInterfaces<PlayerInputService>();
        serviceCollection.RegisterSelfAndInterfaces<PlayerUpdateService>();
        serviceCollection.RegisterSelfAndInterfaces<CharacterDrawService>();
        serviceCollection.RegisterSelfAndInterfaces<EnemyGuiService>();
        
        serviceCollection.AddTransient<EnemyGoalBehavior, IdleEnemyGoalBehavior>();
        serviceCollection.AddTransient<EnemyGoalBehavior, FightPlayerEnemyGoalBehavior>();
        serviceCollection.AddTransient<EnemyGoalBehavior, FightGoalEnemyBehavior>();
        serviceCollection.AddTransient<EnemyGoalBehavior, ChaseGoalBehavior>();
        
        // Menu
        serviceCollection.AddScoped<MenuInput>();
        serviceCollection.AddSingleton<MenuState>();
        serviceCollection.RegisterSelfAndInterfaces<MenuInputService>();
        serviceCollection.RegisterSelfAndInterfaces<MenuUpdateService>();
        serviceCollection.RegisterSelfAndInterfaces<MenuGuiService>(ServiceLifetime.Singleton);
        serviceCollection.RegisterSelfAndInterfaces<GamePausedInputService>();

        // Gui
        serviceCollection.AddSingleton<GuiState>();
        serviceCollection.RegisterSelfAndInterfaces<GuiInputService>();
        serviceCollection.RegisterSelfAndInterfaces<MetricsGuiService>();
        
        // Levels
        serviceCollection.AddSingleton<DoorLookup>();
        serviceCollection.AddSingleton<RoomLookup>();
        serviceCollection.AddSingleton<RoomGraph>();
        serviceCollection.RegisterSelfAndInterfaces<RoomLightService>();
        serviceCollection.RegisterSelfAndInterfaces<DoorService>();
        serviceCollection.RegisterSelfAndInterfaces<LevelGeometryService>();
        serviceCollection.RegisterSelfAndInterfaces<LevelCollisionService>();
        serviceCollection.RegisterSelfAndInterfaces<LevelDrawService>();
        serviceCollection.RegisterSelfAndInterfaces<GoalUpdateService>();
        serviceCollection.RegisterSelfAndInterfaces<LevelGuiService>();
        
        // Navigation
        serviceCollection.RegisterSelfAndInterfaces<NavigationGuiService>();
        
        // Events
        serviceCollection.AddEvent<CharacterDamagedEvent>();
        serviceCollection.AddEvent<CharacterDiedEvent>();
        serviceCollection.AddEvent<CharacterSpawnedEvent>();
        serviceCollection.AddEvent<DoorOpenedEvent>();
    }
}