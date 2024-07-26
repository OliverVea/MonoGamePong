using Microsoft.Xna.Framework;

namespace Shared.Input.Mouse;

public class Mouse
{
    public ButtonState LeftButtonState { get; init; } = new();
    public ButtonState MiddleButtonState { get; init; } = new();
    public ButtonState RightButtonState { get; init; } = new();
    
    public Vector2 Position { get; set; }
    public Vector2 Delta { get; set; }
}