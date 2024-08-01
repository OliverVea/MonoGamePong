using Shared.Geometry.Shapes;

namespace Entombed.Code.Game.Levels;

public class Door
{
    public Id<Door> Id { get; } = Id<Door>.NewId();
    public required LineSegment LineSegment { get; init; }
    public required Id<Room> From { get; init; }
    public required Id<Room> To { get; init; }
    public bool Open { get; set; } = false;
}