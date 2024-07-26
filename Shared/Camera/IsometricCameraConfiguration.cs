using Microsoft.Xna.Framework;

namespace Shared.Camera;

public class IsometricCameraConfiguration
{
    public float InitialZoom { get; set; } = 100f;
    public Vector2 InitialPosition { get; set; }
}