using Microsoft.Xna.Framework;

namespace Shared.Random;

public static class RandomHelper
{
    private static readonly System.Random Random = new System.Random();

    public static float RandomFloat(float from, float to)
    {
        return (float) Random.NextDouble() * (to - from) + from;
    }
    
    public static T PickOne<T>(this IReadOnlyList<T> list)
    {
        var index = Random.Next(list.Count);
        
        return list[index];
    }

    public static Vector2 RandomDirection()
    {
        var angle = RandomFloat(0, MathHelper.TwoPi);
        
        return new Vector2(MathF.Cos(angle), MathF.Sin(angle));
    }
}