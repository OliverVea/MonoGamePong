using Shared.Geometry.Definitions;
using Shared.Geometry.Shapes;

namespace Entombed.Game.Levels;

public class Room
{
    public Id<Room> Id { get; } = Id<Room>.NewId();
    public required LineSegment[] Walls { get; init; }
    public required ShapeInput[] Areas { get; init; }
    public bool Revealed { get; set; } = false;
    public bool Lit { get; set; } = false;
}