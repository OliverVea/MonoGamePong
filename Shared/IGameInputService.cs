using Microsoft.Xna.Framework;

namespace Shared;

public interface IGameInputService<out T>
{
    T GetGameInput(GameTime gameTime);
}