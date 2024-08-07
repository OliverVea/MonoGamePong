using System.Collections.Generic;
using System.Linq;
using Entombed.Game.Levels;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Shared.Navigation;

namespace Entombed.Game.Navigation;

public class NavigationStateService(
    NavigationGraphService navigationGraphService,
    LevelGeometryService levelGeometryService,
    PathfindingService pathfindingService,
    ILogger<NavigationStateService> logger)
{
    private const float NavigationGraphRadius = 0.5f;

    public NavigationState GetNavigationState(Level level)
    {
        var walls = level.Rooms.SelectMany(x => x.Walls).ToArray();
        var areas = level.Rooms.SelectMany(x => x.Areas).ToArray();
        
        var navigationGraph = navigationGraphService.BuildNavigationGraph(walls, areas, NavigationGraphRadius);
        var roomPaths = BuildRoomPaths(navigationGraph, level);

        return new NavigationState
        {
            NavigationGraph = navigationGraph,
            RoomPaths = roomPaths
        };
    }

    private Dictionary<(Id<Room> From, Id<Room> To), NavigationPath> BuildRoomPaths(NavigationGraph navigationGraph, Level level)
    {
        var roomPaths = new Dictionary<(Id<Room> From, Id<Room> To), NavigationPath>();
        var rooms = level.Rooms.ToArray();
        var roomLookup = level.Rooms.ToDictionary(x => x.Id);
        
        for (var i = 0; i < rooms.Length; i++)
        {
            var room = rooms[i];
            for (var j = 0; j < rooms.Length; j++)
            {
                var otherRoom = rooms[j];
                if (room.Id == otherRoom.Id) continue;
                
                var pathResult = GetPathBetweenRooms(navigationGraph, room.Id, otherRoom.Id, roomLookup, level);
                if (pathResult.IsT0)
                {
                    roomPaths.Add((room.Id, otherRoom.Id), pathResult.AsT0);
                    continue;
                }
                
                logger.LogError("Navigating from room {From} to room {To} got error {Error}", i, j, pathResult.AsT1.Value);
            }
        }

        return roomPaths;
    }

    private OneOf<NavigationPath, Error<string>> GetPathBetweenRooms(
        NavigationGraph navigationGraph,
        Id<Room> from,
        Id<Room> to,
        Dictionary<Id<Room>, Room> roomLookup,
        Level level)
    {
        var fromOrigin = roomLookup[from].Origin;
        var toOrigin = roomLookup[to].Origin;
        
        var fromNode = navigationGraph.Nodes
            .OrderBy(x => Vector2.DistanceSquared(fromOrigin, x.Position))
            .FirstOrDefault(x => !levelGeometryService.CollidesWithLevel(fromOrigin, x.Position, level));
        if (fromNode == null) return new Error<string>("Could not find navigation node for 'from' room origin");

        var toNode = navigationGraph.Nodes
            .OrderBy(x => Vector2.DistanceSquared(toOrigin, x.Position))
            .FirstOrDefault(x => !levelGeometryService.CollidesWithLevel(toOrigin, x.Position, level));
        if (toNode == null) return new Error<string>("Could not find navigation node for 'to' room origin");
        
        var pathFindingResult = pathfindingService.FindPath(navigationGraph, fromNode.Id, toNode.Id);
        if (pathFindingResult.IsT0) return pathFindingResult.AsT0;
        
        return new Error<string>("Could not find path between rooms");
    }
}