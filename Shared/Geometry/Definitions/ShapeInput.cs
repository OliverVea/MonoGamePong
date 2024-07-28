using Shared.Geometry.Shapes;

namespace Shared.Geometry.Definitions;

public class ShapeInput : OneOfBase<Circle, Rectangle, Triangle>, IShape
{
    protected ShapeInput(OneOf<Circle, Rectangle, Triangle> input) : base(input)
    {
    }
    
    public static implicit operator ShapeInput(Circle circle) => new(circle);
    public static implicit operator ShapeInput(Rectangle rectangle) => new(rectangle);
    public static implicit operator ShapeInput(Triangle triangle) => new(triangle);
    
    public bool Contains(GeometryInput geometryInput)
    {
        return Match(
            circle => circle.Contains(geometryInput),
            rectangle => rectangle.Contains(geometryInput),
            triangle => triangle.Contains(geometryInput)
        );
    }

    public bool Intersects(LineInput lineInput)
    {
        return Match(
            circle => circle.Intersects(lineInput),
            rectangle => rectangle.Intersects(lineInput),
            triangle => triangle.Intersects(lineInput)
        );
    }

    public bool Overlaps(ShapeInput shapeInput)
    {
        return Match(
            circle => circle.Overlaps(shapeInput),
            rectangle => rectangle.Overlaps(shapeInput),
            triangle => triangle.Overlaps(shapeInput)
        );
    }
}