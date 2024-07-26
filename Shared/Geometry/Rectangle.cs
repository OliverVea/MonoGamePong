using Microsoft.Xna.Framework;

namespace Shared.Geometry;

public readonly struct Rectangle
{
    public required Vector2 Center { get; init; }
    public required Vector2 Size { get; init; }

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
}