using System.Threading.Tasks;
using Entombed.Game;
using Entombed.Game.Characters.Players;
using Entombed.Game.Levels.Generation;
using Entombed.Game.Navigation;
using Shared.Lifetime;
using Shared.Scenes;

namespace Entombed.Loading;

public class LoadingUpdateService(
    LoadingState loadingState,
    SceneManager sceneManager,
    LevelGeneratorService levelGeneratorService,
    NavigationStateService navigationStateService) : IUpdateService, IStartupService
{
    public void Startup()
    {
        loadingState.LoadingTask.Value = Task.Run(Load);
    }
    
    public void Update()
    {
        if (!loadingState.LoadingTask.Value.IsCompleted || loadingState.GameScene is not { } gameScene) return;
        
        sceneManager.Transition(gameScene);
    }
    
    private void Load()
    {
        loadingState.LoadingText = "Loading level...";
        
        var level = levelGeneratorService.GenerateLevel();
        var player = new Player();
        
        loadingState.LoadingText = "Generating navigation...";
        
        var navigationState = navigationStateService.GetNavigationState(level);
        
        loadingState.GameScene = new GameScene(level, player, navigationState);
    }
}