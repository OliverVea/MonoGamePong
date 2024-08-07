using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;

namespace Shared.Geometry.Helpers;

internal static partial class IntersectsHelper
{
    public static bool Intersects(in LineSegment lineSegment, in Vector2 point)
    {
        var v1 = point - lineSegment.Start;
        var v2 = lineSegment.End - lineSegment.Start;
        var v3 = point - lineSegment.End;

        var dot1 = Vector2.Dot(v1, v2);
        var dot2 = Vector2.Dot(v3, v2);

        var t = MathHelper.Clamp(dot1 / v2.LengthSquared(), 0, 1);
        
        var projection = lineSegment.Start + v2 * t;
        
        return Vector2.DistanceSquared(point, projection) < 0.001f;
    }

    public static bool Intersects(in LineSegment lineSegment, in LineSegment otherLineSegment)
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