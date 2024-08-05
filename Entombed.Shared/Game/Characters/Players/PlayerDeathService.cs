using Entombed.Game.Characters.Events;
using Entombed.MainMenu;
using Microsoft.Extensions.Logging;
using Shared.Lifetime;
using Shared.Scenes;

namespace Entombed.Game.Characters.Players;

public class PlayerDeathService(Player player, IEventObserver<CharacterDiedEvent> characterDiedEvent, SceneManager sceneManager, ILogger<PlayerDeathService> logger) : IStartupService
{
    public void Startup()
    {
        characterDiedEvent.Subscribe(OnCharacterDied);
    }

    private void OnCharacterDied(CharacterDiedEvent e)
    {
        if (e.TargetId != player.Id) return;
        
        logger.LogInformation("Player died");
        
        sceneManager.Transition<MainMenuScene>();
    }
}