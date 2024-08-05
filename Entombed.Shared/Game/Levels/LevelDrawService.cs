using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Camera;

namespace Entombed.Game.Levels;

public class LevelDrawService(Level level, RoomLookup roomLookup, DoorLookup doorLookup, IsometricCamera isometricCamera, ILogger<LevelDrawService> logger)
{
    public void Draw(SpriteBatch spriteBatch)
    {
        var revealedRooms = roomLookup.Values.Where(room => room.Revealed).ToArray();

        foreach (var room in revealedRooms)
        {
            DrawRoom(room, spriteBatch);
        }

        foreach (var door in doorLookup.Values)
        {
            DrawDoor(door, spriteBatch);
        }

        DrawGoal(spriteBatch);
        DrawStairs(spriteBatch);
    }

    private void DrawRoom(Room room, SpriteBatch spriteBatch)
    {
        var walls = room.Walls;
            
        foreach (var wall in walls)
        {
            var wallScreenSpace = isometricCamera.WorldToScreen(wall);
                
            spriteBatch.DrawLineSegment(wallScreenSpace, Color.White, room.Lit ? 3 : 1);
        }
    }

    private void DrawDoor(Door door, SpriteBatch spriteBatch)
    {
        if (door.Open) return;
        
        var rooms = new[] {door.From, door.To}.Select(roomLookup.Get).ToArray();
        if (rooms.All(room => !room.Revealed)) return;

        var doorScreenSpace = isometricCamera.WorldToScreen(door.Line);
                
        spriteBatch.DrawLine(doorScreenSpace.Start, doorScreenSpace.End, Color.Red);
    }

    private void DrawGoal(SpriteBatch spriteBatch)
    {
        var goalScreenSpace = isometricCamera.WorldToScreen(level.Goal);
        
        spriteBatch.DrawCircle(goalScreenSpace, 5, 16, Color.Yellow);
    }
    
    private void DrawStairs(SpriteBatch spriteBatch)
    {
        var stairsRoomResult = roomLookup.GetRoomWithPosition(level.Stairs);

        if (stairsRoomResult.IsT1)
        {
            logger.LogWarning("Stairs are not in a room");
            return;
        }
        
        var stairsRoom = roomLookup.Get(stairsRoomResult.AsT0);
        if (!stairsRoom.Revealed) return;
        
        var stairsScreenSpace = isometricCamera.WorldToScreen(level.Stairs);
        
        spriteBatch.DrawCircle(stairsScreenSpace, 5, 16, Color.Green);
    }
}