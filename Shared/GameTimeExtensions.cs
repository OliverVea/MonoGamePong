using Microsoft.Xna.Framework;

namespace Shared;

public static class GameTimeExtensions
{
    public static float DeltaTime(this GameTime gameTime) => (float)gameTime.ElapsedGameTime.TotalSeconds;
}