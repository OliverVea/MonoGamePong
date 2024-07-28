using Shared.Geometry.Shapes;

namespace Shared.Geometry.Helpers;

internal static partial class IntersectsHelper
{
    public static bool Intersects(Triangle thisTriangle, LineSegment otherLineSegment)
    {
        var lineSegments = new[]
        {
            new LineSegment(thisTriangle.A, thisTriangle.B),
            new LineSegment(thisTriangle.B, thisTriangle.C),
            new LineSegment(thisTriangle.C, thisTriangle.A)
        };
        
        return lineSegments.Any(lineSegment => Intersects(lineSegment, otherLineSegment));
    }

    public static bool Intersects(Triangle thisTriangle, Rectangle otherRectangle)
    {
        var lineSegments = new[]
        {
            new LineSegment(otherRectangle.TopLeft, otherRectangle.TopRight),
            new LineSegment(otherRectangle.TopRight, otherRectangle.BottomRight),
            new LineSegment(otherRectangle.BottomRight, otherRectangle.BottomLeft),
            new LineSegment(otherRectangle.BottomLeft, otherRectangle.TopLeft)
        };
        
        return lineSegments.Any(lineSegment => Intersects(thisTriangle, lineSegment));
    }

    public static bool Intersects(Triangle thisTriangle, Triangle otherLineSegment)
    {
        var lineSegments = new[]
        {
            new LineSegment(otherLineSegment.A, otherLineSegment.B),
            new LineSegment(otherLineSegment.B, otherLineSegment.C),
            new LineSegment(otherLineSegment.C, otherLineSegment.A)
        };
        
        return lineSegments.Any(lineSegment => Intersects(thisTriangle, lineSegment));
    }
}