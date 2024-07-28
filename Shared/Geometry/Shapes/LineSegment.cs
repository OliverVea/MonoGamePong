using Microsoft.Xna.Framework;

namespace Shared.Geometry.Shapes;

public readonly partial struct LineSegment(Vector2 start, Vector2 end)
{
    public LineSegment(float x1, float y1, float x2, float y2)
        : this(new Vector2(x1, y1), new Vector2(x2, y2)) { }
    
    public Vector2 Start { get; } = start;
    public Vector2 End { get; } = end;
}