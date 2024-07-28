using Microsoft.Xna.Framework;
using Shared.Geometry.Definitions;

namespace Shared.Geometry.Shapes;

public readonly partial struct Circle(Vector2 center, float radius) : IShape
{
    public Vector2 Center { get; } = center;
    public float Radius { get; } = radius;
    private readonly float _radiusSquared = radius * radius;
}