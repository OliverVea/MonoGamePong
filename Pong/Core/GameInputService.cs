using Microsoft.Xna.Framework.Input;
using Shared.Lifetime;

namespace Pong.Core;

public class GameInputService(GameInput gameInput) : IInputService
{
    public void Input()
    {
        gameInput.LeftPlayerInput.Direction = GetInputDirection(Keys.W, Keys.S);
        gameInput.RightPlayerInput.Direction = GetInputDirection(Keys.Up, Keys.Down);
    }
    
    private static InputDirection GetInputDirection(Keys upKey, Keys downKey)
    {
        var up = Keyboard.GetState().IsKeyDown(upKey);
        var down = Keyboard.GetState().IsKeyDown(downKey);

        if (up && down) return InputDirection.None;
        if (up) return InputDirection.Up;
        if (down) return InputDirection.Down;
        
        return InputDirection.None;
    }
}