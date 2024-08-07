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

    public static int RandomInt(int from, int to)
    {
        return Random.Next(from, to + 1);
    }

    public static T RandomElement<T>(ICollection<T> collection)
    {
        var index = Random.Next(collection.Count());
        
        return collection.ElementAt(index);
    }

    public static T RandomElement<T>(Span<T> span)
    {
        var index = Random.Next(span.Length);
        
        return span[index];
    }

    public static float Normal(float dev) => Normal(0, dev);

    public static float Normal(float mean, float dev)
    {
        var u1 = 1.0f - Random.NextSingle();
        var u2 = 1.0f - Random.NextSingle();
        var randStdNormal = MathF.Sqrt(-2.0f * MathF.Log(u1)) * MathF.Sin(MathHelper.TwoPi * u2);
        
        return mean + dev * randStdNormal;
    }
}