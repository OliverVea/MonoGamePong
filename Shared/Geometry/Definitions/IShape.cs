using Microsoft.Xna.Framework;

namespace Shared.Geometry.Definitions;

public interface IShape
{
    bool Contains(GeometryInput geometryInput);
    bool Intersects(LineInput lineInput);
    bool Overlaps(ShapeInput shapeInput);
    Vector2 SamplePoint(float padding = 0);
}