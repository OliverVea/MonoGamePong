using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Shared.Geometry.Helpers;

internal static partial class ContainsHelper
{
    public static bool Contains(Triangle triangle, Vector2 point)
    {
        var v0 = triangle.C - triangle.A;
        var v1 = triangle.B - triangle.A;
        var v2 = point - triangle.A;
        
        var dot00 = Vector2.Dot(v0, v0);
        var dot01 = Vector2.Dot(v0, v1);
        var dot02 = Vector2.Dot(v0, v2);
        
        var dot11 = Vector2.Dot(v1, v1);
        var dot12 = Vector2.Dot(v1, v2);
        
        var invertedDenominator = 1 / (dot00 * dot11 - dot01 * dot01);
        
        var u = (dot11 * dot02 - dot01 * dot12) * invertedDenominator;
        var v = (dot00 * dot12 - dot01 * dot02) * invertedDenominator;
        
        return u >= 0 && v >= 0 && u + v < 1;
    }

    public static bool Contains(Triangle thisTriangle, LineSegment lineSegment)
    {
        return Contains(thisTriangle, lineSegment.Start) && Contains(thisTriangle, lineSegment.End);
    }

    public static bool Contains(Triangle thisTriangle, Circle circle)
    {
        return Contains(thisTriangle, circle.Center) && Contains(thisTriangle, circle.Center + new Vector2(circle.Radius, 0)) &&
               Contains(thisTriangle, circle.Center + new Vector2(0, circle.Radius)) && Contains(thisTriangle, circle.Center + new Vector2(-circle.Radius, 0)) &&
               Contains(thisTriangle, circle.Center + new Vector2(0, -circle.Radius));
    }

    public static bool Contains(Triangle thisTriangle, Rectangle rectangle)
    {
        return Contains(thisTriangle, rectangle.TopLeft) && Contains(thisTriangle, rectangle.TopRight) &&
               Contains(thisTriangle, rectangle.BottomLeft) && Contains(thisTriangle, rectangle.BottomRight);
    }

    public static bool Contains(Triangle thisTriangle, Triangle lineSegment)
    {
        return Contains(thisTriangle, lineSegment.A) && Contains(thisTriangle, lineSegment.B) && Contains(thisTriangle, lineSegment.C);
    }
}