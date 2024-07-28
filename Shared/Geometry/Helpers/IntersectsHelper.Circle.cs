using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Shared.Geometry.Helpers;

internal static partial class IntersectsHelper
{
    public static bool Intersects(Circle circle, LineSegment lineSegment)
    {
        var closestPoint = ClosestPoint(lineSegment, circle.Center);

        return Vector2.Distance(circle.Center, closestPoint) <= circle.Radius;
        
        Vector2 ClosestPoint(LineSegment lineSegment1, Vector2 circleCenter)
        {
            var line = lineSegment1.End - lineSegment1.Start;
            var lineLength = line.Length();
            var lineDirection = line / lineLength;
            var point = circleCenter - lineSegment1.Start;
            var projection = Vector2.Dot(point, lineDirection);
            projection = Math.Clamp(projection, 0, lineLength);
            return lineSegment1.Start + lineDirection * projection;
        }
    }

    public static bool Intersects(Circle circle, Circle otherCircle)
    {
        var distance = Vector2.Distance(circle.Center, otherCircle.Center);
        if (distance > circle.Radius + otherCircle.Radius) return false;

        return distance >= Math.Abs(circle.Radius - otherCircle.Radius);
    }

    public static bool Intersects(Circle thisCircle, Rectangle rectangle)
    {
        var edges = new[]
        {
            new LineSegment(rectangle.TopLeft, rectangle.TopRight),
            new LineSegment(rectangle.TopRight, rectangle.BottomRight),
            new LineSegment(rectangle.BottomRight, rectangle.BottomLeft),
            new LineSegment(rectangle.BottomLeft, rectangle.TopLeft)
        };
        
        return edges.Any(edge => Intersects(thisCircle, edge));
    }

    public static bool Intersects(Circle thisCircle, Triangle triangle)
    {
        var edges = new[]
        {
            new LineSegment(triangle.A, triangle.B),
            new LineSegment(triangle.B, triangle.C),
            new LineSegment(triangle.C, triangle.A)
        };
        
        return edges.Any(edge => Intersects(thisCircle, edge));
    }
}