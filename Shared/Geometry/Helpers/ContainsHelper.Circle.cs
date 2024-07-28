using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Shared.Geometry.Helpers;

internal partial class ContainsHelper
{
    public static bool Contains(Circle circle, Vector2 point)
    {
        return Vector2.Distance(circle.Center, point) <= circle.Radius;
    }

    public static bool Contains(Circle circle, Triangle triangle)
    {
        return Contains(circle, triangle.A) && Contains(circle, triangle.B) && Contains(circle, triangle.C);
    }

    public static bool Contains(Circle circle, Rectangle rectangle)
    {
        return Contains(circle, rectangle.TopLeft) && Contains(circle, rectangle.TopRight) &&
               Contains(circle, rectangle.BottomLeft) && Contains(circle, rectangle.BottomRight);
    }

    public static bool Contains(Circle circle, Circle otherCircle)
    {
        return Vector2.Distance(circle.Center, otherCircle.Center) + otherCircle.Radius <= circle.Radius;
    }

    public static bool Contains(Circle circle, LineSegment lineSegment)
    {
        return Contains(circle, lineSegment.Start) && Contains(circle, lineSegment.End);
    }
}