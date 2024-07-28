using Shared.Geometry.Shapes;

namespace Shared.Geometry.Definitions;

public class LineInput : OneOfBase<LineSegment, Circle, Rectangle, Triangle>
{
    protected LineInput(OneOf<LineSegment, Circle, Rectangle, Triangle> input) : base(input)
    {
    }
    
    public static implicit operator LineInput(LineSegment lineSegment) => new(lineSegment);
    public static implicit operator LineInput(Circle circle) => new(circle);
    public static implicit operator LineInput(Rectangle rectangle) => new(rectangle);
    public static implicit operator LineInput(Triangle triangle) => new(triangle);
}