using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Entombed.Game.Levels;

public class Level
{
    public Vector2 Goal { get; set; } = Vector2.Zero;
    public Vector2 Stairs { get; set; } = Vector2.Zero;
    public required IReadOnlyCollection<Room> Rooms { get; init; }
    public IReadOnlyCollection<Door> Doors { get; init; } = [];
}