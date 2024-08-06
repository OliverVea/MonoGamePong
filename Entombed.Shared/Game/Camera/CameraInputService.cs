using Entombed.Game.Menu;
using Shared.Lifetime;
using Keyboard = Shared.Input.Keyboard.Keyboard;

namespace Entombed.Game.Camera;

public class CameraInputService(
    GamePaused gamePaused,
    GameInputScheme gameInputScheme,
    Keyboard keyboard,
    CameraInput cameraInput) : IInputService
{
    public bool Active => !gamePaused.Paused;
    
    public void Input()
    {
        cameraInput.CameraZoomAction = CameraZoomAction.None;
        
        if (keyboard.Get(gameInputScheme.ZoomInKey).Down) cameraInput.CameraZoomAction = CameraZoomAction.ZoomIn;
        if (keyboard.Get(gameInputScheme.ZoomOutKey).Down) cameraInput.CameraZoomAction = CameraZoomAction.ZoomOut;
    }
}