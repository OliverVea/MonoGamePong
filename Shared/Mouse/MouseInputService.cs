using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shared.Lifetime;

namespace Shared.Mouse;

public class MouseInputService(Mouse mouse, GraphicsDevice graphicsDevice) : IInputService
{
    public void Input()
    {
        var mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
        
        var yOffset = graphicsDevice.Viewport.Height;
        var rectifiedY = yOffset - mouseState.Y;
        
        mouse.Delta = new(mouseState.X - mouse.Position.X, rectifiedY - mouse.Position.Y);
        mouse.Position = new(mouseState.X, rectifiedY);

        UpdateMouseButtonState(mouseState.LeftButton, mouse.LeftButton);
        UpdateMouseButtonState(mouseState.MiddleButton, mouse.MiddleButton);
        UpdateMouseButtonState(mouseState.RightButton, mouse.RightButton);
    }

    private void UpdateMouseButtonState(ButtonState inputState, MouseButtonState previousState)
    {
        previousState.Down = inputState == ButtonState.Pressed;
        previousState.Pressed = inputState == ButtonState.Pressed && !previousState.Down;
        previousState.Released = inputState == ButtonState.Released && previousState.Down;
    }
}