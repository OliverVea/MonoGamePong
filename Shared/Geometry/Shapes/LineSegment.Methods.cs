using Microsoft.Xna.Framework;
using Shared.Geometry.Helpers;

namespace Shared.Geometry.Shapes;

public readonly partial struct LineSegment
{
    public bool Intersects(OneOf<Vector2, LineSegment> input)
    {
        var thisLineSegment = this;
        
        return input.Match(
            point => IntersectsHelper.Intersects(thisLineSegment, point),
            line => IntersectsHelper.Intersects(thisLineSegment, line)
        );
    }

    public double Distance(Vector2 point)
    {
        return DistanceHelper.Distance(this, point);
    }
}