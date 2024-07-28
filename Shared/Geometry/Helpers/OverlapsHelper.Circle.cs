using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Shared.Geometry.Helpers;

internal static partial class OverlapsHelper
{
    public static bool Overlaps(Circle circle, Circle otherCircle)
    {
        var distance = Vector2.Distance(circle.Center, otherCircle.Center);
        return distance < circle.Radius + otherCircle.Radius;
    }

    public static bool Overlaps(Circle circle, Rectangle rectangle)
    {
        var corners = new[]
        {
            rectangle.TopLeft,
            rectangle.TopRight,
            rectangle.BottomLeft,
            rectangle.BottomRight
        };
        
        return corners.Any(corner => ContainsHelper.Contains(circle, corner));
    }

    public static bool Overlaps(Circle circle, Triangle triangle)
    {
        var corners = new[]
        {
            triangle.A,
            triangle.B,
            triangle.C
        };
        
        return corners.Any(corner => ContainsHelper.Contains(circle, corner));
    }
}