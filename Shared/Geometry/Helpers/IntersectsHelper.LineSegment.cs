using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;

namespace Shared.Geometry.Helpers;

internal static partial class IntersectsHelper
{
    public static bool Intersects(LineSegment lineSegment, Vector2 point)
    {
        var distanceStart = Vector2.DistanceSquared(lineSegment.Start, point);
        var distanceEnd = Vector2.DistanceSquared(lineSegment.End, point);
        
        var distance = Vector2.DistanceSquared(lineSegment.Start, lineSegment.End);

        var delta = distanceStart + distanceEnd - distance;
        
        return Math.Abs(delta) < 0.0001f;
    }

    public static bool Intersects(LineSegment lineSegment, LineSegment otherLineSegment)
    {
        var termA = (otherLineSegment.End.Y - otherLineSegment.Start.Y) * (lineSegment.End.X - lineSegment.Start.X);
        var termB = (otherLineSegment.End.X - otherLineSegment.Start.X) * (lineSegment.End.Y - lineSegment.Start.Y);
        var delta = termA - termB;
        
        if (Math.Abs(delta) < 0.0001f) return false;

        termA = (otherLineSegment.End.X - otherLineSegment.Start.X) * (lineSegment.Start.Y - otherLineSegment.Start.Y);
        termB = (otherLineSegment.End.Y - otherLineSegment.Start.Y) * (lineSegment.Start.X - otherLineSegment.Start.X);
        
        var beta = termA - termB;
        beta /= delta;
        
        termA = (lineSegment.End.Y - lineSegment.Start.Y) * (lineSegment.Start.X - otherLineSegment.Start.X);
        termB = (lineSegment.End.X - lineSegment.Start.X) * (lineSegment.Start.Y - otherLineSegment.Start.Y);
        
        var alpha = termA - termB;
        alpha /= delta;

        return alpha is >= 0 and <= 1 && beta is >= 0 and <= 1;
    }
    
}