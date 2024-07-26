using Microsoft.Xna.Framework;

namespace Shared.Geometry;

public readonly struct Circle(Vector2 center, float radius)
{
    public Vector2 Center { get; } = center;
    public float Radius { get; } = radius;
}