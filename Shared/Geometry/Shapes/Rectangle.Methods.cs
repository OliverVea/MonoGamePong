using Shared.Geometry.Definitions;
using Shared.Geometry.Helpers;

namespace Shared.Geometry.Shapes;

public readonly partial struct Rectangle
{
    public bool Contains(GeometryInput geometryInput)
    {
        var thisRectangle = this;
        
        return geometryInput.Match(
            point => ContainsHelper.Contains(thisRectangle, point),
            line => ContainsHelper.Contains(thisRectangle, line),
            circle => ContainsHelper.Contains(circle, thisRectangle),
            rectangle => ContainsHelper.Contains(thisRectangle, rectangle),
            triangle => ContainsHelper.Contains(triangle, thisRectangle)
            );
    }

    public bool Intersects(LineInput lineInput)
    {
        var thisRectangle = this;
        
        return lineInput.Match(
            line => IntersectsHelper.Intersects(thisRectangle, line),
            circle => IntersectsHelper.Intersects(circle, thisRectangle),
            rectangle => IntersectsHelper.Intersects(thisRectangle, rectangle),
            triangle => IntersectsHelper.Intersects(triangle, thisRectangle)
            );
    }

    public bool Overlaps(ShapeInput shapeInput)
    {
        var thisRectangle = this;
        
        return shapeInput.Match(
            circle => OverlapsHelper.Overlaps(circle, thisRectangle),
            rectangle => OverlapsHelper.Overlaps(thisRectangle, rectangle),
            triangle => OverlapsHelper.Overlaps(triangle, thisRectangle)
            );
    }
}