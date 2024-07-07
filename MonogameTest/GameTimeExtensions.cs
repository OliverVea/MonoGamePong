using Microsoft.Xna.Framework;

namespace MonogameTest;

public static class GameTimeExtensions
{
    public static float DeltaTime(this GameTime gameTime) => (float)gameTime.ElapsedGameTime.TotalSeconds;
}