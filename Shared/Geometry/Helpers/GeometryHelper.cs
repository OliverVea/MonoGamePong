using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Shared.Geometry.Helpers;

internal static class GeometryHelper
{
    public static bool Overlaps(LineSegment a, Vector2 b)
    {
        var t = ((b.X - a.Start.X) * (a.End.X - a.Start.X) + (b.Y - a.Start.Y) * (a.End.Y - a.Start.Y)) /
                (a.End - a.Start).LengthSquared();
        
        if (t < 0 || t > 1) return false;
        
        var closest = a.Start + (a.End - a.Start) * t;
        
        return closest == b;
    }
    public static bool Overlaps(LineSegment a, LineSegment b)
    {
        var p0 = a.Start;
        var p1 = a.End;
        var p2 = b.Start;
        var p3 = b.End;
        
        var s1 = p1 - p0;
        var s2 = p3 - p2;
        
        var s = (-s1.Y * (p0.X - p2.X) + s1.X * (p0.Y - p2.Y)) / (-s2.X * s1.Y + s1.X * s2.Y);
        var t = (s2.X * (p0.Y - p2.Y) - s2.Y * (p0.X - p2.X)) / (-s2.X * s1.Y + s1.X * s2.Y);
        
        return s is >= 0 and <= 1 && t is >= 0 and <= 1;
    }
    public static bool Overlaps(LineSegment a, Circle b)
    {
        var closest = ClosestPointOnLineSegment(a, b.Center);
        
        return Vector2.DistanceSquared(closest, b.Center) <= b.Radius * b.Radius;

        static Vector2 ClosestPointOnLineSegment(LineSegment lineSegment, Vector2 bCenter)
        {
            var a = lineSegment.Start;
            var b = lineSegment.End;
        
            var ap = bCenter - a;
            var ab = b - a;
        
            var t = Vector2.Dot(ap, ab) / Vector2.Dot(ab, ab);
        
            if (t < 0) return a;
            if (t > 1) return b;
        
            return a + ab * t;
        }
    }
    public static bool Overlaps(LineSegment a, Rectangle b)
    {
        if (Overlaps(b, a.Start) || Overlaps(b, a.End)) return true;
        return b.Edges.Any(edge => Overlaps(a, edge));
    }
    public static bool Overlaps(Circle a, Vector2 b) => Vector2.DistanceSquared(a.Center, b) <= a.Radius * a.Radius;
    public static bool Overlaps(Circle a, LineSegment b) => Overlaps(b, a);
    public static bool Overlaps(Circle a, Circle b)
    {
        return Vector2.DistanceSquared(a.Center, b.Center) <= (a.Radius + b.Radius) * (a.Radius + b.Radius);
    }
    public static bool Overlaps(Circle a, Rectangle b)
    {
        if (Overlaps(b, a.Center)) return true;
        
        var closest = ClosestPointOnRectangle(b, a.Center);
        
        return Vector2.DistanceSquared(closest, a.Center) <= a.Radius * a.Radius;
        
        static Vector2 ClosestPointOnRectangle(Rectangle rectangle, Vector2 bCenter)
        {
            var closest = new Vector2(
                MathHelper.Clamp(bCenter.X, rectangle.Left, rectangle.Right),
                MathHelper.Clamp(bCenter.Y, rectangle.Top, rectangle.Bottom)
            );
            
            return closest;
        }
    }
    public static bool Overlaps(Rectangle a, Vector2 b)
    {
        return b.X >= a.Left && b.X <= a.Right && b.Y >= a.Top && b.Y <= a.Bottom;
    }
    public static bool Overlaps(Rectangle a, LineSegment b) => Overlaps(b, a);
    public static bool Overlaps(Rectangle a, Circle b) => Overlaps(b, a);
    public static bool Overlaps(Rectangle a, Rectangle b)
    {
        return a.Left <= b.Right && a.Right >= b.Left && a.Top <= b.Bottom && a.Bottom >= b.Top;
    }
    public static bool Overlaps(Vector2 a, Vector2 b) => a == b;
    public static bool Overlaps(Vector2 a, LineSegment b) => Overlaps(b, a);
    public static bool Overlaps(Vector2 a, Circle b) => Overlaps(b, a);
    public static bool Overlaps(Vector2 a, Rectangle b) => Overlaps(b, a);

    public static float Distance(Vector2 playerPosition, LineSegment doorPosition)
    {
        var closest = ClosestPointOnLineSegment(doorPosition, playerPosition);
        
        return Vector2.Distance(playerPosition, closest);

        static Vector2 ClosestPointOnLineSegment(LineSegment lineSegment, Vector2 bCenter)
        {
            var a = lineSegment.Start;
            var b = lineSegment.End;
        
            var ap = bCenter - a;
            var ab = b - a;
        
            var t = Vector2.Dot(ap, ab) / Vector2.Dot(ab, ab);
        
            if (t < 0) return a;
            if (t > 1) return b;
        
            return a + ab * t;
        }
    }
}