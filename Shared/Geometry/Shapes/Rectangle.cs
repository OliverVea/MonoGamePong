using Microsoft.Xna.Framework;
using Shared.Geometry.Definitions;

namespace Shared.Geometry.Shapes;

public readonly partial struct Rectangle(Vector2 center, Vector2 size) : IShape
{
    public Vector2 Center { get; } = center;
    public Vector2 Size { get; } = size;

    public float Left => Center.X - Size.X / 2;
    public float Right => Center.X + Size.X / 2;
    public float Top => Center.Y - Size.Y / 2;
    public float Bottom => Center.Y + Size.Y / 2;
    
    public Vector2 TopLeft => Center - Size / 2;
    public Vector2 TopRight => Center + new Vector2(Size.X / 2, -Size.Y / 2);
    public Vector2 BottomLeft => Center + new Vector2(-Size.X / 2, Size.Y / 2);
    public Vector2 BottomRight => Center + Size / 2;
    
    public LineSegment[] Edges =>
    [
        new LineSegment(TopLeft, TopRight),
        new LineSegment(TopRight, BottomRight),
        new LineSegment(BottomRight, BottomLeft),
        new LineSegment(BottomLeft, TopLeft)
    ];

    public double Width => Size.X;
    public double Height => Size.Y;
}