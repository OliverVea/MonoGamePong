using System.Collections.Generic;

namespace Entombed.Code.Game.Levels;

public class Level
{
    public required IReadOnlyCollection<Room> Rooms { get; init; }
    public IReadOnlyCollection<Door> Doors { get; init; } = [];
}