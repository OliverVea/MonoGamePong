using ShaderSandbox.Core.Models;

namespace ShaderSandbox.Core.Services;

public class GameLogicService(GameState gameState, GameProperties gameProperties) : IGameLogicService<GameInput>
{
    public void Update(GameInput gameInput)
    {
    }
}