using System.Linq;
using Microsoft.Xna.Framework;

namespace Entombed.Code.Game.Levels;

public class RoomGeometryService
{
    public bool InsideRoom(Room room, Vector2 position)
    {
        return room.Area.Any(x => x.Contains(position));
    }
}