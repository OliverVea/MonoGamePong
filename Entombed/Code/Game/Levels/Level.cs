using Microsoft.Xna.Framework;
using Shared.Geometry;

namespace Entombed.Code.Game.Levels;

public class Level
{
    public required Room[] Rooms { get; init; }
    public Door[] Doors { get; init; } = [];
}

public class Room
{
    public required LineSegment[] Walls { get; init; }
}

public class Door
{
    public required LineSegment Position { get; init; }
    public required Room From { get; init; }
    public required Room To { get; init; }
}

public static class Levels
{
    private static readonly Room Level1Room1 = new()
    {
        Walls =
        [
            new LineSegment(-3, -3, -0.5f, -3),
            new LineSegment(0.5f, -3, 3, -3),
            new LineSegment(-3, -3, -3, 3),
            new LineSegment(-3, 3, 3, 3),
            new LineSegment(3, 3, 3, -3)
        ]
    };

    private static readonly Room Level1Room2 = new()
    {
        Walls = [
            new LineSegment(-3, -3, -0.5f, -3),
            new LineSegment(0.5f, -3, 3, -3),
            new LineSegment(-3, -3, -3, -6),
            new LineSegment(-3, -6, 3, -6),
            new LineSegment(3, -6, 3, -3)
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
                Position = new LineSegment(new Vector2(0.5f, -3), new Vector2(0.5f, 3))
            }
        ]
    };
}