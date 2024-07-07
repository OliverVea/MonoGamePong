using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonogameTest;

public class GameInputService
{
    public GameInput GetGameInput(GameTime gameTime)
    {
        var leftPlayerInput = new PlayerInput
        {
            Direction = GetInputDirection(Keys.W, Keys.S)
        };
        
        var rightPlayerInput = new PlayerInput
        {
            Direction = GetInputDirection(Keys.Up, Keys.Down)
        };

        return new GameInput
        {
            LeftPlayerInput = leftPlayerInput,
            RightPlayerInput = rightPlayerInput,
            GameTime = gameTime
        };
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