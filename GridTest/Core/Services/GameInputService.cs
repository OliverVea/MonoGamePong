using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShaderSandbox.Core.Models;

namespace ShaderSandbox.Core.Services;

public class GameInputService(GameState gameState) : IGameInputService<GameInput>
{
    public GameInput GetGameInput(GameTime gameTime)
    {
        gameState.GameTime = gameTime;

        var mouseState = Mouse.GetState();
        
        return new GameInput
        {
            MousePosition = new Vector2(mouseState.X, 450 - mouseState.Y)
        };
    }
}