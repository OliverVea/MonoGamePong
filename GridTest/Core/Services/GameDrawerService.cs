using Microsoft.Xna.Framework.Graphics;
using ShaderSandbox.Core.Models;

namespace ShaderSandbox.Core.Services;

public class GameDrawerService(
    GameState gameState,
    SpriteBatch spriteBatch) : IGameDrawerService<GameInput>
{
    public void Draw(GameInput gameInput)
    {
    }
}