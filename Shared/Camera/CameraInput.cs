using Microsoft.Xna.Framework;

namespace Shared.Camera;

public class CameraInput
{
    public OneOf<None, SetZoom, ZoomVelocity> Zoom { get; set; }
    public OneOf<None, SetPosition, MoveVelocity> Position { get; set; }
    
    public class SetZoom(float value)
    {
        public float Value { get; } = value;
    }
    
    public class ZoomVelocity(float value)
    {
        public float Value { get; } = value;
    }
    
    public class SetPosition(Vector2 value)
    {
        public Vector2 Value { get; } = value;
    }
    
    public class MoveVelocity(Vector2 value)
    {
        public Vector2 Value { get; } = value;
    }
}