using Microsoft.Xna.Framework;

namespace Shared.Mouse;

public class Mouse
{
    public MouseButtonState LeftButton { get; init; } = new();
    public MouseButtonState MiddleButton { get; init; } = new();
    public MouseButtonState RightButton { get; init; } = new();
    
    public Vector2 Position { get; set; }
    public Vector2 Delta { get; set; }
}