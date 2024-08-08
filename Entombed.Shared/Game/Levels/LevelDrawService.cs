using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Camera;
using Shared.Geometry.Shapes;

namespace Entombed.Game.Levels;

public class LevelDrawService(Level level, RoomLookup roomLookup, DoorLookup doorLookup, IsometricCamera isometricCamera, ILogger<LevelDrawService> logger)
{
    private const float StairsRadius = 0.05f;
    private const float GoalRadius = 0.05f;
    private const float WallWidth = 0.05f;
    private const float LitWallScale = 2f;
    private static readonly Color LitRoomColor = new(0.1f, 0.1f, 0f, 0.15f);
    
    public void Draw(SpriteBatch spriteBatch)
    {
        var screenSpaceWallWidth = isometricCamera.WorldToScreen(WallWidth);

        foreach (var door in doorLookup.Values)
        {
            DrawDoor(door, spriteBatch);
        }

        foreach (var room in roomLookup.Values)
        {
            DrawRoom(room, spriteBatch, screenSpaceWallWidth);
        }

        DrawGoal(spriteBatch);
        DrawStairs(spriteBatch);
    }

    private void DrawRoom(Room room, SpriteBatch spriteBatch, float screenSpaceWallWidth)
    {
        if (!room.Revealed) return;

        if (room.Lit)
        {
            var screenSpaceAreas = room.Areas.Select(isometricCamera.WorldToScreen).ToArray();
            foreach (var screenSpaceArea in screenSpaceAreas)
            {
                screenSpaceArea.Switch(
                    circle => { },
                    rectangle => spriteBatch.FillRectangle(rectangle.ToXnaRectangle(), LitRoomColor),
                    triangle => { });
            }
        }
        
        var walls = room.Walls;
            
        foreach (var wall in walls)
        {
            var wallScreenSpace = isometricCamera.WorldToScreen(wall);
            var direction = wall.Center - room.Origin;
            if (MathF.Abs(direction.X) > MathF.Abs(direction.Y)) direction.Y = 0;
            else direction.X = 0;
            direction = Vector2.Normalize(direction);
            
            direction = new Vector2(direction.X, -direction.Y);
            
            var wallOutwardOffset = -direction * screenSpaceWallWidth / 2f;
            
            var wallDirection = Vector2.Normalize(wall.End - wall.Start);
            wallDirection = new Vector2(wallDirection.X, -wallDirection.Y);
            var wallExtensionOffset = wallDirection * screenSpaceWallWidth / 2f;

            wallScreenSpace = new LineSegment(
                wallScreenSpace.Start + wallOutwardOffset - wallExtensionOffset,
                wallScreenSpace.End + wallOutwardOffset + wallExtensionOffset);
            
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