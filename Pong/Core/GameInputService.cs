using Microsoft.Xna.Framework.Input;
using Shared.Lifetime;

namespace Pong.Core;

public class GameInputService(GameInput gameInput) : IInputService
{
    private bool _wasTextShaderKeyDown;
    
    public void Input()
    {
        gameInput.LeftPlayerInput.Direction = GetInputDirection(Keys.W, Keys.S);
        gameInput.RightPlayerInput.Direction = GetInputDirection(Keys.Up, Keys.Down);
        
        GetTextShaderInput();
    }

    private void GetTextShaderInput()
    {
        var isTextShaderKeyDown = Keyboard.GetState().IsKeyDown(Keys.T);
        if (isTextShaderKeyDown && !_wasTextShaderKeyDown)
        {
            gameInput.UseTextShader = !gameInput.UseTextShader;
        }
        
        _wasTextShaderKeyDown = isTextShaderKeyDown;
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