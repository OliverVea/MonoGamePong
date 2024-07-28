using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Shared.Geometry.Definitions;

public class GeometryInput : OneOfBase<Vector2, LineSegment, Circle, Rectangle, Triangle>
{
    protected GeometryInput(OneOf<Vector2, LineSegment, Circle, Rectangle, Triangle> input) : base(input)
    {
    }
    
    public static implicit operator GeometryInput(Vector2 vector2) => new(vector2);
    public static implicit operator GeometryInput(LineSegment lineSegment) => new(lineSegment);
    public static implicit operator GeometryInput(Circle circle) => new(circle);
}