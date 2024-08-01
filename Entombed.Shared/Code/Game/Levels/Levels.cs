using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Entombed.Code.Game.Levels;

public static class Levels
{
    private static readonly Room Level1Room1 = new()
    {
        Walls =
        [
            new LineSegment(-3, -3, -0.5f, -3),
            new LineSegment(0.5f, -3, 3, -3),
            new LineSegment(3, -3, 3, 3),
            new LineSegment(3, 3, -3, 3),
            new LineSegment(-3, 3, -3, -3),
        ],
        Areas = [
            new Rectangle(new Vector2(0, 0), new Vector2(6, 6))
        ],
        Revealed = true,
        Lit = true
    };

    private static readonly Room Level1Room2 = new()
    {
        Walls = [
            new LineSegment(-3, -3, -0.5f, -3),
            new LineSegment(0.5f, -3, 3, -3),
            new LineSegment(-3, -3, -3, -6),
            new LineSegment(-3, -6, 3, -6),
            new LineSegment(3, -6, 3, -3)
        ],
        Areas = [
            new Rectangle(new Vector2(0, -4.5f), new Vector2(6, 3))
        ]
    };
    
    private static readonly Door Level1Door1To2 = new()
    {
        From = Level1Room1.Id,
        To = Level1Room2.Id,
        LineSegment = new LineSegment(new Vector2(-0.5f, -3), new Vector2(0.5f, -3))
    };
    
    public static readonly Level Level1 = new()
    {
        Rooms = [
            Level1Room1,
            Level1Room2
        ],
        Doors = [
            Level1Door1To2
        ]
    };
}