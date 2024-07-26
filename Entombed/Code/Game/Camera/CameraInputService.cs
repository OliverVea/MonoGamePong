using Microsoft.Xna.Framework.Input;
using Shared.Lifetime;
using Keyboard = Shared.Input.Keyboard.Keyboard;

namespace Entombed.Code.Game.Camera;

public class CameraInputService(Keyboard keyboard, CameraInput cameraInput) : IInputService
{
    private const Keys ZoomInKey = Keys.Q;
    private const Keys ZoomOutKey = Keys.E;
    
    public void Input()
    {
        cameraInput.CameraZoomAction = CameraZoomAction.None;
        
        if (keyboard.Get(ZoomInKey).Down) cameraInput.CameraZoomAction = CameraZoomAction.ZoomIn;
        if (keyboard.Get(ZoomOutKey).Down) cameraInput.CameraZoomAction = CameraZoomAction.ZoomOut;
    }
}