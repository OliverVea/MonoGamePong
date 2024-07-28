using Shared.Geometry.Shapes;

namespace Shared.Geometry.Helpers;

internal static partial class IntersectsHelper
{
    public static bool Intersects(Rectangle rectangle, LineSegment lineSegment)
    {
        var lineSegments = new[]
        {
            new LineSegment(rectangle.TopLeft, rectangle.TopRight),
            new LineSegment(rectangle.TopRight, rectangle.BottomRight),
            new LineSegment(rectangle.BottomRight, rectangle.BottomLeft),
            new LineSegment(rectangle.BottomLeft, rectangle.TopLeft)
        };
        
        return lineSegments.Any(line => Intersects(line, lineSegment));
    }

    public static bool Intersects(Rectangle rectangle, Rectangle otherRectangle)
    {
        var lineSegments = new[]
        {
            new LineSegment(otherRectangle.TopLeft, otherRectangle.TopRight),
            new LineSegment(otherRectangle.TopRight, otherRectangle.BottomRight),
            new LineSegment(otherRectangle.BottomRight, otherRectangle.BottomLeft),
            new LineSegment(otherRectangle.BottomLeft, otherRectangle.TopLeft)
        };
        
        return lineSegments.Any(line => Intersects(rectangle, line));
    }
}