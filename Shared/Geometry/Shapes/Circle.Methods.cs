using Shared.Geometry.Definitions;
using Shared.Geometry.Helpers;

namespace Shared.Geometry.Shapes;

public readonly partial struct Circle
{
    public bool Contains(GeometryInput geometryInput)
    {
        var thisCircle = this;
        
        return geometryInput.Match(
                vector2 => ContainsHelper.Contains(thisCircle, vector2),
                lineSegment => ContainsHelper.Contains(thisCircle, lineSegment),
                circle => ContainsHelper.Contains(thisCircle, circle),
                rectangle => ContainsHelper.Contains(thisCircle, rectangle),
                triangle => ContainsHelper.Contains(thisCircle, triangle)
            );
    }

    public bool Intersects(LineInput lineInput)
    {
        var thisCircle = this;
        
        return lineInput.Match(
                lineSegment => IntersectsHelper.Intersects(thisCircle, lineSegment),
                circle => IntersectsHelper.Intersects(circle, thisCircle),
                rectangle => IntersectsHelper.Intersects(thisCircle, rectangle),
                triangle => IntersectsHelper.Intersects(thisCircle, triangle)
            );
    }

    public bool Overlaps(ShapeInput shapeInput)
    {
        var thisCircle = this;
        
        return shapeInput.Match(
                circle => OverlapsHelper.Overlaps(thisCircle, circle),
                rectangle => OverlapsHelper.Overlaps(thisCircle, rectangle),
                triangle => OverlapsHelper.Overlaps(thisCircle, triangle)
            );
    }
}