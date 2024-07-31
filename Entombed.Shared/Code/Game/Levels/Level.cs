using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shared.Geometry.Definitions;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Entombed.Code.Game.Levels;

public class Level
{
    public required IReadOnlyDictionary<Id<Room>, Room> Rooms { get; init; }
    public IReadOnlyDictionary<Id<Door>, Door> Doors { get; init; } = new Dictionary<Id<Door>, Door>();
}

public class Room
{
    public Id<Room> Id { get; } = Id<Room>.NewId();
    public required LineSegment[] Walls { get; init; }
    public required ShapeInput[] Area { get; init; }
    public bool Revealed { get; set; } = false;
    public bool Lit { get; set; } = false;
}

public class Door
{
    public Id<Door> Id { get; } = Id<Door>.NewId();
    public required LineSegment LineSegment { get; init; }
    public required Id<Room> From { get; init; }
    public required Id<Room> To { get; init; }
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
        Area = [
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
        Rooms = new Dictionary<Id<Room>, Room>
        {
            {Level1Room1.Id, Level1Room1},
            {Level1Room2.Id, Level1Room2}
        },
        Doors = new Dictionary<Id<Door>, Door>()
        {
            {Level1Door1To2.Id, Level1Door1To2}
        }
    };
}