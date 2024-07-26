using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using Shared.Camera;

namespace Entombed.Code.Game.Characters;

public class PlayerDrawService(Player player, IsometricCamera isometricCamera)
{
    private const int CircleSides = 32;

    public void Draw(SpriteBatch spriteBatch)
    {
        var screenSpacePosition = isometricCamera.WorldToScreen(player.Position);
        var screenSpaceRadius = isometricCamera.WorldToScreen(player.Radius);
        
        spriteBatch.DrawCircle(screenSpacePosition, screenSpaceRadius, CircleSides, Color.White);
    }
}