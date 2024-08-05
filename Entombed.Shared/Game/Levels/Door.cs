using Shared.Geometry.Shapes;

namespace Entombed.Game.Levels;

public class Door
{
    public Id<Door> Id { get; } = Id<Door>.NewId();
    public required LineSegment Line { get; init; }
    public required Id<Room> From { get; init; }
    public required Id<Room> To { get; init; }
    public bool Open { get; set; } = false;
}