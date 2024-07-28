using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using Shared.Camera;

namespace Entombed.Code.Game.Levels;

public class LevelDrawService(Level level, IsometricCamera isometricCamera)
{
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var room in level.Rooms)
        {
            var walls = room.Walls;
            
            foreach (var wall in walls)
            {
                var wallScreenSpace = isometricCamera.WorldToScreen(wall);
                
                spriteBatch.DrawLine(wallScreenSpace.Start, wallScreenSpace.End, Color.White, room.Revealed ? 3 : 1);
            }
        }

        foreach (var door in level.Doors)
        {
            if (door.Open) continue;

            var doorScreenSpace = isometricCamera.WorldToScreen(door.LineSegment);
                
            spriteBatch.DrawLine(doorScreenSpace.Start, doorScreenSpace.End, Color.Red);
        }
    }
}