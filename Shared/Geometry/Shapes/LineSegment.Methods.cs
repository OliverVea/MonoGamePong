using Microsoft.Xna.Framework;
using Shared.Geometry.Helpers;

namespace Shared.Geometry.Shapes;

public readonly partial struct LineSegment
{
    public bool Intersects(in OneOf<Vector2, LineSegment> input)
    {
        var thisLineSegment = this;
        
        return input.Match(
            point => IntersectsHelper.Intersects(thisLineSegment, point),
            line => IntersectsHelper.Intersects(thisLineSegment, line)
        );
    }

    public double Distance(in Vector2 point)
    {
        return DistanceHelper.Distance(this, point);
    }
}