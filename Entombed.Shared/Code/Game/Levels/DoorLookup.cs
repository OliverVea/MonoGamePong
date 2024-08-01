using Shared.Content;

namespace Entombed.Code.Game.Levels;

public class DoorLookup : Lookup<Door>
{
    public DoorLookup(Level level)
    {
        foreach (var door in level.Doors)
        {
            Dictionary.TryAdd(door.Id, door);
        }
    }
}