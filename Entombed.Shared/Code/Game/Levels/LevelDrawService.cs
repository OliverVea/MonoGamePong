using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Camera;

namespace Entombed.Code.Game.Levels;

public class LevelDrawService(Level level, IsometricCamera isometricCamera)
{
    public void Draw(SpriteBatch spriteBatch)
    {
        var revealedRooms = level.Rooms.Where(room => room.Revealed).ToArray();

        foreach (var room in revealedRooms)
        {
            DrawRoom(room, spriteBatch);
        }

        foreach (var door in level.Doors)
        {
            DrawDoor(door, spriteBatch);
        }
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

        var doorScreenSpace = isometricCamera.WorldToScreen(door.LineSegment);
                
        spriteBatch.DrawLine(doorScreenSpace.Start, doorScreenSpace.End, Color.Red);
    }
}