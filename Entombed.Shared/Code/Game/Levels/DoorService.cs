using Microsoft.Extensions.Logging;

namespace Entombed.Code.Game.Levels;

public class DoorService(IEventInvoker<DoorOpenedEvent> doorOpenedEvent, Level level, ILogger<DoorService> logger)
{
    public void OpenDoor(Id<Door> id)
    {
        if (!level.Doors.TryGetValue(id, out var door)) return;
        
        if (door.Open) return;
        
        door.Open = true;
        
        if (level.Rooms.TryGetValue(door.From, out var fromRoom)) fromRoom.Revealed = true;
        else logger.LogWarning("Room {RoomId} not found", door.From);
        
        if (level.Rooms.TryGetValue(door.To, out var toRoom)) toRoom.Revealed = true;
        else logger.LogWarning("Room {RoomId} not found", door.To);
        
        doorOpenedEvent.Invoke(new DoorOpenedEvent(id));
    }
}