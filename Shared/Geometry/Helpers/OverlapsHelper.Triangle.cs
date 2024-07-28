using Shared.Geometry.Shapes;

namespace Shared.Geometry.Helpers;

internal static partial class OverlapsHelper
{
    public static bool Overlaps(Triangle triangle, Rectangle rectangle)
    {
        var corners = new[]
        {
            rectangle.TopLeft,
            rectangle.TopRight,
            rectangle.BottomLeft,
            rectangle.BottomRight
        };
        
        return corners.Any(corner => ContainsHelper.Contains(triangle, corner));
    }

    public static bool Overlaps(Triangle triangle, Triangle otherTriangle)
    {
        var corners = new[]
        {
            otherTriangle.A,
            otherTriangle.B,
            otherTriangle.C
        };
        
        return corners.Any(corner => ContainsHelper.Contains(triangle, corner));
    }
}