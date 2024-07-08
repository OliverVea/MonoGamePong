using Microsoft.Xna.Framework;

namespace Shared;

public static class Vector2Extensions
{
    public static Vector2 FlipHorizontal(this Vector2 vector)
    {
        return new Vector2(-vector.X, vector.Y);
    }
    
    public static Vector2 FlipVertical(this Vector2 vector)
    {
        return new Vector2(vector.X, -vector.Y);
    }
}