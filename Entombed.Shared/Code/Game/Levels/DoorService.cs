using Microsoft.Extensions.Logging;

namespace Entombed.Code.Game.Levels;

public class DoorService(IEventInvoker<DoorOpenedEvent> doorOpenedEvent, DoorLookup doorLookup, RoomLookup roomLookup, ILogger<DoorService> logger)
{
    public void OpenDoor(Id<Door> id)
    {
        if (!doorLookup.TryGet(id, out var door)) return;
        
        if (door.Open) return;
        
        door.Open = true;
        
        if (roomLookup.TryGet(door.From, out var fromRoom)) fromRoom.Revealed = true;
        else logger.LogWarning("Room {RoomId} not found", door.From);
        
        if (roomLookup.TryGet(door.To, out var toRoom)) toRoom.Revealed = true;
        else logger.LogWarning("Room {RoomId} not found", door.To);
        
        doorOpenedEvent.Invoke(new DoorOpenedEvent(id));
    }
}