using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

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
    
    public Vector2 SamplePoint(float padding = 0)
    {
        return Match(
            circle => circle.SamplePoint(padding),
            rectangle => rectangle.SamplePoint(padding),
            triangle => triangle.SamplePoint(padding)
        );
    }

    public Vector2 Center()
    {
        return Match(
            circle => circle.Center,
            rectangle => rectangle.Center,
            triangle => triangle.Center
        );
    }
}