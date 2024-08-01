using Microsoft.Xna.Framework;
using Shared.Geometry.Definitions;
using Shared.Geometry.Helpers;

namespace Shared.Geometry.Shapes;

public readonly partial struct Triangle
{
    public bool Contains(GeometryInput geometryInput)
    {
        var thisTriangle = this;
        
        return geometryInput.Match(
            point => ContainsHelper.Contains(thisTriangle, point),
            line => ContainsHelper.Contains(thisTriangle, line),
            circle => ContainsHelper.Contains(thisTriangle, circle),
            rectangle => ContainsHelper.Contains(thisTriangle, rectangle),
            triangle => ContainsHelper.Contains(thisTriangle, triangle)
        );
    }

    public bool Intersects(LineInput lineInput)
    {
        var thisTriangle = this;
        
        return lineInput.Match(
            line => IntersectsHelper.Intersects(thisTriangle, line),
            circle => IntersectsHelper.Intersects(circle, thisTriangle),
            rectangle => IntersectsHelper.Intersects(thisTriangle, rectangle),
            triangle => IntersectsHelper.Intersects(thisTriangle, triangle)
        );
    }

    public bool Overlaps(ShapeInput shapeInput)
    {
        var thisTriangle = this;
        
        return shapeInput.Match(
            circle => OverlapsHelper.Overlaps(circle, thisTriangle),
            rectangle => OverlapsHelper.Overlaps(thisTriangle, rectangle),
            triangle => OverlapsHelper.Overlaps(thisTriangle, triangle)
        );
    }

    public Vector2 SamplePoint(float padding)
    {
        return SamplePointHelper.SamplePoint(this, padding);
    }

    public void Deconstruct(out Vector2 a, out Vector2 b, out Vector2 c)
    {
        a = A;
        b = B;
        c = C;
    }
}