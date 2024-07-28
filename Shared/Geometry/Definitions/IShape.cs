using Shared.Geometry.Shapes;

namespace Shared.Geometry.Definitions;

public interface IShape
{
    bool Contains(GeometryInput geometryInput);
    bool Intersects(LineInput lineInput);
    bool Overlaps(ShapeInput shapeInput);
}