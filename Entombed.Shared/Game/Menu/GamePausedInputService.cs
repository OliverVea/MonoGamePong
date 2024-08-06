using Shared.Lifetime;
using Keyboard = Shared.Input.Keyboard.Keyboard;

namespace Entombed.Game.Menu;

public class GamePausedInputService(GameInputScheme gameInputScheme, GamePaused gamePaused, Keyboard keyboard) : IInputService
{
    public void Input()
    {
        var pauseKeyPressed = keyboard.Get(gameInputScheme.PauseKey).Pressed;
        if (pauseKeyPressed) gamePaused.Pause = !gamePaused.Paused;
    }
}