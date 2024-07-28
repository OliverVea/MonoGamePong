using Microsoft.Xna.Framework;
using Shared.Geometry.Definitions;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Entombed.Code.Game.Levels;

public class Level
{
    public required Room[] Rooms { get; init; }
    public Door[] Doors { get; init; } = [];
}

public class Room
{
    public required LineSegment[] Walls { get; init; }
    public required ShapeInput[] Area { get; init; }
    public bool Revealed { get; set; } = false;
}

public class Door
{
    public required LineSegment LineSegment { get; init; }
    public required Room From { get; init; }
    public required Room To { get; init; }
    public bool Open { get; set; } = false;
}

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
        Area = [
            new Rectangle(new Vector2(0, 0), new Vector2(6, 6))
        ],
        Revealed = true
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
        Area = [
            new Rectangle(new Vector2(0, -4.5f), new Vector2(6, 3))
        ]
    };
    
    public static readonly Level Level1 = new()
    {
        Rooms =
        [
            Level1Room1,
            Level1Room2
        ],
        Doors = [
            new Door
            {
                From = Level1Room1,
                To = Level1Room2,
                LineSegment = new LineSegment(new Vector2(-0.5f, -3), new Vector2(0.5f, -3))
            }
        ]
    };
}