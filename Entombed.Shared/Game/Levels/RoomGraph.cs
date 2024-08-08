using System;
using System.Collections.Generic;
using System.Linq;

namespace Entombed.Game.Levels;

public class RoomGraph
{
    private readonly IReadOnlyDictionary<Id<Room>, IReadOnlyCollection<Id<Room>>> _adjacentRooms;
    private readonly IReadOnlyDictionary<Id<Room>, int> _stepsFromStart;

    public RoomGraph(IReadOnlyCollection<Room> rooms, IReadOnlyCollection<Door> doors)
    {
        
        var adjacentRooms = new Dictionary<Id<Room>, IReadOnlyCollection<Id<Room>>>();
        
        foreach (var room in rooms)
        {
            var adjacentRoomIds = new List<Id<Room>>();
            foreach (var door in doors)
            {
                if (door.From == room.Id) adjacentRoomIds.Add(door.To);
                else if (door.To == room.Id) adjacentRoomIds.Add(door.From);
            }
            adjacentRooms.Add(room.Id, adjacentRoomIds);
            
        }
        
        _adjacentRooms = adjacentRooms;
        
        var stepsFromStart = new Dictionary<Id<Room>, int>();
        
        var startRoom = rooms.First();
        var frontier = new Queue<Id<Room>>();
        frontier.Enqueue(startRoom.Id);
        
        stepsFromStart.Add(startRoom.Id, 0);
        
        while (frontier.TryDequeue(out var currentRoomId))
        {
            var steps = stepsFromStart[currentRoomId];
            
            foreach (var adjacentRoomId in adjacentRooms[currentRoomId])
            {
                if (stepsFromStart.ContainsKey(adjacentRoomId)) continue;
                
                stepsFromStart.Add(adjacentRoomId, steps + 1);
                frontier.Enqueue(adjacentRoomId);
            }
        }
        
        _stepsFromStart = stepsFromStart;
    }
    
    public RoomGraph(RoomLookup roomLookup, DoorLookup doorLookup) : this(roomLookup.Values, doorLookup.Values)
    {
    }
    
    public IReadOnlyCollection<Id<Room>> GetAdjacentRooms(Id<Room> roomId)
    {
        return _adjacentRooms.TryGetValue(roomId, out var adjacentRooms) ? adjacentRooms : Array.Empty<Id<Room>>();
    }
    
    public int GetStepsFromStart(Id<Room> roomId)
    {
        return _stepsFromStart[roomId];
    }
}