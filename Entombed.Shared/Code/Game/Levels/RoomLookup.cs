using Shared.Content;

namespace Entombed.Code.Game.Levels;

public class RoomLookup : Lookup<Room>
{
    public RoomLookup(Level level)
    {
        foreach (var room in level.Rooms)
        {
            Dictionary.TryAdd(room.Id, room);
        }
    }
}