using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Shared.Random;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Shared.Geometry.Helpers;

public static partial class SamplePointHelper
{
    private static readonly System.Random Random = new();
    
    public static Vector2 SamplePoint(Triangle triangle, float padding)
    {
        var (a, b, c) = triangle;
        
        var pointA = a;
        var pointB = b;
        var pointC = c;
        
        var areaA = Vector2.Distance(pointB, pointC) * Vector2.Distance(pointA, pointC) / 2;
        var areaB = Vector2.Distance(pointA, pointC) * Vector2.Distance(pointB, pointC) / 2;
        var areaC = Vector2.Distance(pointA, pointB) * Vector2.Distance(pointA, pointC) / 2;
        
        var totalArea = areaA + areaB + areaC;
        
        var random = Random.NextDouble();
        
        if (random < areaA / totalArea)
        {
            var point = SamplePoint(a, b, padding);
            return point;
        }
        
        if (random < (areaA + areaB) / totalArea)
        {
            var point = SamplePoint(b, c, padding);
            return point;
        }
        
        var pointBc = SamplePoint(b, c, padding);
        var pointCa = SamplePoint(c, a, padding);
        
        return Random.Next(0, 2) == 0 ? pointBc : pointCa;
    }

    private static Vector2 SamplePoint(Vector2 a, Vector2 b, float padding)
    {
        var x = RandomHelper.RandomFloat(a.X + padding, b.X - padding);
        var y = RandomHelper.RandomFloat(a.Y + padding, b.Y - padding);
        
        return new Vector2(x, y);
    }

    public static Vector2 SamplePoint(Rectangle rectangle, float padding)
    {
        if (padding >= rectangle.Width || padding >= rectangle.Height) return rectangle.Center;
        
        var x = RandomHelper.RandomFloat(rectangle.Left + padding, rectangle.Right - padding);
        var y = RandomHelper.RandomFloat(rectangle.Top + padding, rectangle.Bottom - padding);
        
        return new Vector2(x, y);
    }

    public static Vector2 SamplePoint(Circle circle, float padding)
    {
        if (padding >= circle.Radius) return circle.Center;
        
        var angle = RandomHelper.RandomFloat(0, MathHelper.TwoPi);
        var radius = RandomHelper.RandomFloat(0, circle.Radius - padding);
        
        var x = circle.Center.X + radius * MathF.Cos(angle);
        var y = circle.Center.Y + radius * MathF.Sin(angle);
        
        return new Vector2(x, y);
    }
}