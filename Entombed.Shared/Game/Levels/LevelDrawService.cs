using System.Linq;
using System.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Camera;

namespace Entombed.Game.Levels;

public class LevelDrawService(Level level, RoomLookup roomLookup, DoorLookup doorLookup, IsometricCamera isometricCamera, ILogger<LevelDrawService> logger)
{
    private const float StairsRadius = 0.05f;
    private const float GoalRadius = 0.05f;
    private const float WallWidth = 0.05f;
    private const float LitWallScale = 2f;
    
    public void Draw(SpriteBatch spriteBatch)
    {
        var revealedRooms = roomLookup.Values.Where(room => room.Revealed).ToArray();
        
        var screenSpaceWallWidth = isometricCamera.WorldToScreen(WallWidth);

        foreach (var room in revealedRooms)
        {
            DrawRoom(room, spriteBatch, screenSpaceWallWidth);
        }

        foreach (var door in doorLookup.Values)
        {
            DrawDoor(door, spriteBatch);
        }

        DrawGoal(spriteBatch);
        DrawStairs(spriteBatch);
    }

    private void DrawRoom(Room room, SpriteBatch spriteBatch, float screenSpaceWallWidth)
    {
        var walls = room.Walls;
        
        if (room.Lit) screenSpaceWallWidth *= LitWallScale;
            
        foreach (var wall in walls)
        {
            var wallScreenSpace = isometricCamera.WorldToScreen(wall);
            var direction = Vector2.Normalize(wall.End - wall.Start);
            spriteBatch.DrawLineSegment(wallScreenSpace, Color.White, screenSpaceWallWidth);
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
        
        var screenSpaceGoalRadius = isometricCamera.WorldToScreen(GoalRadius);
        
        spriteBatch.DrawCircle(goalScreenSpace, screenSpaceGoalRadius, 16, Color.Yellow);
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
        
        var screenSpaceStairsPosition = isometricCamera.WorldToScreen(level.Stairs);
        var screenSpaceStairsRadius = isometricCamera.WorldToScreen(StairsRadius);
        
        spriteBatch.DrawCircle(screenSpaceStairsPosition, screenSpaceStairsRadius, 16, Color.Green);
    }
}