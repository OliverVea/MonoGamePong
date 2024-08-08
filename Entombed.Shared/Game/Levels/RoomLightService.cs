using System.Collections.Generic;
using System.Linq;

namespace Entombed.Game.Levels;

public class RoomLightService(RoomLookup roomLookup, RoomGraph roomGraph)
{

    public void ToggleRoomLight(Id<Room> roomId)
    {
        var roomsThatCanBeLit = GetRoomsThatCanBeLit();
        
        if (roomsThatCanBeLit.Contains(roomId))
        {
            roomLookup.Get(roomId).Lit = !roomLookup.Get(roomId).Lit;
        }
        
        UpdateRoomLights();
    }
    
    private void UpdateRoomLights()
    {
        var roomsThatCanBeLit = GetRoomsThatCanBeLit();
        
        foreach (var room in roomLookup.Values)
        {
            room.Lit &= roomsThatCanBeLit.Contains(room.Id);
        }
    }

    private IReadOnlySet<Id<Room>> GetRoomsThatCanBeLit()
    {
        var startRoom = roomLookup.Values.First();
        
        var visitedRooms = new HashSet<Id<Room>>();
        HashSet<Id<Room>> allowedRooms = [startRoom.Id];
        var frontier = new Queue<Id<Room>>();
        
        frontier.Enqueue(startRoom.Id);
        
        while (frontier.TryDequeue(out var currentRoomId))
        {
            if (visitedRooms.Contains(currentRoomId)) continue;
            
            allowedRooms.Add(currentRoomId);
            visitedRooms.Add(currentRoomId);
            
            var currentRoom = roomLookup.Get(currentRoomId);
            if (!currentRoom.Lit) continue;
            
            foreach (var adjacentRoomId in roomGraph.GetAdjacentRooms(currentRoomId))
            {
                frontier.Enqueue(adjacentRoomId);
            }
        }
        
        return allowedRooms;
    }
}