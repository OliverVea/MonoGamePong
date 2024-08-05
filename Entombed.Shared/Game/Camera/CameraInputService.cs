using Shared.Lifetime;
using Keyboard = Shared.Input.Keyboard.Keyboard;

namespace Entombed.Game.Camera;

public class CameraInputService(GameInputScheme gameInputScheme, Keyboard keyboard, CameraInput cameraInput) : IInputService
{
    public void Input()
    {
        cameraInput.CameraZoomAction = CameraZoomAction.None;
        
        if (keyboard.Get(gameInputScheme.ZoomInKey).Down) cameraInput.CameraZoomAction = CameraZoomAction.ZoomIn;
        if (keyboard.Get(gameInputScheme.ZoomOutKey).Down) cameraInput.CameraZoomAction = CameraZoomAction.ZoomOut;
    }
}