using System.Linq;
using Entombed.Game.Camera;
using Entombed.Game.Characters;
using Entombed.Game.Characters.Enemies;
using Entombed.Game.Characters.Enemies.GoalBehaviors;
using Entombed.Game.Characters.Events;
using Entombed.Game.Characters.Players;
using Entombed.Game.Gui;
using Entombed.Game.Levels;
using Entombed.Game.Levels.Serialization;
using Entombed.Game.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Camera;
using Shared.Extensions;
using Shared.Navigation;
using Shared.Scenes;
using Shared.Screen;
using CameraUpdateService = Entombed.Game.Camera.CameraUpdateService;

namespace Entombed.Game;

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
        serviceCollection.AddTransient<CharacterDamageService>();
        serviceCollection.AddSingleton<CharacterLookup>();
        serviceCollection.RegisterService<EnemySpawningService>();
        serviceCollection.RegisterService<EnemyUpdateService>();
        serviceCollection.AddSingleton<Player>();
        serviceCollection.AddScoped<PlayerInput>();
        serviceCollection.AddSingleton<GameInputScheme>();
        serviceCollection.RegisterService<PlayerInputService>();
        serviceCollection.RegisterService<PlayerUpdateService>();
        serviceCollection.RegisterSelfAndInterfaces<CharacterDrawService>();
        serviceCollection.RegisterSelfAndInterfaces<EnemyGuiService>();
        
        serviceCollection.AddTransient<EnemyGoalBehavior, IdleEnemyGoalBehavior>();
        serviceCollection.AddTransient<EnemyGoalBehavior, FightPlayerEnemyGoalBehavior>();
        serviceCollection.AddTransient<EnemyGoalBehavior, FightGoalEnemyBehavior>();
        serviceCollection.AddTransient<EnemyGoalBehavior, ChaseGoalBehavior>();

        // Gui
        serviceCollection.RegisterInterfaces<GuiInputService>();
        serviceCollection.AddSingleton<GuiState>();
        serviceCollection.RegisterService<MetricsGuiService>();
        
        // Levels
        serviceCollection.AddSingleton<DoorLookup>();
        serviceCollection.AddSingleton<RoomLookup>();
        serviceCollection.AddTransient<DoorService>();
        serviceCollection.RegisterService<LevelCollisionService>();
        serviceCollection.AddTransient<LevelGeometryService>();
        
        var levelDeserializer = new LevelDeserializer();
        var level = levelDeserializer.Deserialize("I:\\My_Temp\\test_level_stuff\\example.xml");

        var firstRoom = level.Rooms.First();
        firstRoom.Revealed = true;
        firstRoom.Lit = true;
        
        serviceCollection.AddSingleton<Level>(_ => level);
        
        serviceCollection.RegisterSelfAndInterfaces<LevelDrawService>();
        
        // Navigation
        serviceCollection.AddSingleton<NavigationState>();
        serviceCollection.RegisterService<NavigationStartupService>();
        serviceCollection.RegisterSelfAndInterfaces<NavigationGuiService>();
        serviceCollection.RegisterSelfAndInterfaces<NavigationService>();
        
        // Events
        serviceCollection.AddEvent<CharacterDamagedEvent>();
        serviceCollection.AddEvent<CharacterDiedEvent>();
        serviceCollection.AddEvent<CharacterSpawnedEvent>();
        serviceCollection.AddEvent<DoorOpenedEvent>();
    }
}