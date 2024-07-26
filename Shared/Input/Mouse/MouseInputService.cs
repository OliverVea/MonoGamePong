using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Lifetime;

namespace Shared.Input.Mouse;

public class MouseInputService(Mouse mouse, GraphicsDevice graphicsDevice) : IInputService
{
    public void Input()
    {
        var mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        
        var yOffset = graphicsDevice.Viewport.Height;
        var rectifiedY = yOffset - mouseState.Y;
        
        mouse.Delta = new Vector2(mouseState.X - mouse.Position.X, rectifiedY - mouse.Position.Y);
        mouse.Position = new Vector2(mouseState.X, rectifiedY);

        UpdateMouseButtonState(mouseState.LeftButton, mouse.LeftButtonState);
        UpdateMouseButtonState(mouseState.MiddleButton, mouse.MiddleButtonState);
        UpdateMouseButtonState(mouseState.RightButton, mouse.RightButtonState);
    }

    private static void UpdateMouseButtonState(Microsoft.Xna.Framework.Input.ButtonState inputState, ButtonState previous)
    {
        previous.Down = inputState == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
        previous.Pressed = inputState == Microsoft.Xna.Framework.Input.ButtonState.Pressed && !previous.Down;
        previous.Released = inputState == Microsoft.Xna.Framework.Input.ButtonState.Released && previous.Down;
    }
}