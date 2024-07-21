using Microsoft.Xna.Framework;

namespace Shared.Extensions;

public static class GameTimeExtensions
{
    public static float DeltaTime(this GameTime gameTime) => (float)gameTime.ElapsedGameTime.TotalSeconds;
    
    public static void Copy(this GameTime gameTime, GameTime other)
    {
        gameTime.TotalGameTime = other.TotalGameTime;
        gameTime.ElapsedGameTime = other.ElapsedGameTime;
        gameTime.IsRunningSlowly = other.IsRunningSlowly;
    }
}