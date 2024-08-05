using System.Linq;
using Microsoft.Xna.Framework;
using Shared.Content;

namespace Entombed.Game.Levels;

public class RoomLookup : Lookup<Room>
{
    public RoomLookup(Level level)
    {
        foreach (var room in level.Rooms)
        {
            Dictionary.TryAdd(room.Id, room);
        }
    }

    public OneOf<Id<Room>, NotFound> GetRoomWithPosition(Vector2 position)
    {
        var room = Dictionary.Values.FirstOrDefault(room => room.Areas.Any(a => a.Contains(position)));
        if (room == null) return new NotFound();
        
        return room.Id;
    }
}