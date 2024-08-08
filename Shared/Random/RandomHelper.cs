using Microsoft.Xna.Framework;

namespace Shared.Random;

public static class RandomHelper
{
    private static readonly System.Random Random = new System.Random();

    public static float RandomFloat(float from, float to)
    {
        return (float) Random.NextDouble() * (to - from) + from;
    }

    public static Vector2 RandomDirection()
    {
        var angle = RandomFloat(0, MathHelper.TwoPi);
        
        return new Vector2(MathF.Cos(angle), MathF.Sin(angle));
    }
    
    private static readonly Vector2[] CardinalDirections =
    {
        Vector2.UnitX,
        -Vector2.UnitX,
        Vector2.UnitY,
        -Vector2.UnitY
    };
    public static Vector2 RandomCardinalDirection()
    {
        return PickOne(CardinalDirections);
    }

    public static int RandomInt(int from, int to)
    {
        return Random.Next(from, to + 1);
    }

    public static T PickOne<T>(IReadOnlyCollection<T> collection)
    {
        var index = Random.Next(collection.Count);
        
        return collection.ElementAt(index);
    }

    public static T PickOne<T>(Span<T> span)
    {
        var index = Random.Next(span.Length);
        
        return span[index];
    }

    public static T PickOne<T>(IReadOnlyCollection<T> collection, Func<T, float> weightPredicate)
    {
        var totalWeight = collection.Sum(weightPredicate);
        var randomValue = RandomFloat(0, totalWeight);
        
        foreach (var item in collection)
        {
            var weight = weightPredicate(item);
            if (randomValue < weight) return item;
            
            randomValue -= weight;
        }
        
        return collection.Last();
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