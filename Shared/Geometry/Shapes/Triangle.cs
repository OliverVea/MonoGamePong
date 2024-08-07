using Microsoft.Xna.Framework;
using Shared.Geometry.Definitions;

namespace Shared.Geometry.Shapes;

public readonly partial struct Triangle(Vector2 a, Vector2 b, Vector2 c) : IShape
{
    public Triangle(float x1, float y1, float x2, float y2, float x3, float y3)
        : this(new Vector2(x1, y1), new Vector2(x2, y2), new Vector2(x3, y3)) { }
    
    public Vector2 A { get; } = a;
    public Vector2 B { get; } = b;
    public Vector2 C { get; } = c;
    public Vector2 Center => (A + B + C) / 3f;
}