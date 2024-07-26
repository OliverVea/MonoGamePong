namespace Entombed.Code.Game.Camera;

public class CameraInput
{
    public CameraZoomAction CameraZoomAction { get; set; }
}

public enum CameraZoomAction
{
    None,
    ZoomIn,
    ZoomOut
}