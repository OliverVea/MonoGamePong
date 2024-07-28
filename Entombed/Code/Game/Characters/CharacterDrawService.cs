using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using Shared.Camera;

namespace Entombed.Code.Game.Characters;

public class CharacterDrawService(CharacterLookup characterLookup, IsometricCamera isometricCamera)
{
    private const int CircleSides = 32;

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var character in characterLookup.Values)
        {
            DrawCharacter(spriteBatch, character);
        }
    }

    private void DrawCharacter(SpriteBatch spriteBatch, Character character)
    {
        var screenSpacePosition = isometricCamera.WorldToScreen(character.Position);
        var screenSpaceRadius = isometricCamera.WorldToScreen(character.Radius);
        
        spriteBatch.DrawCircle(screenSpacePosition, screenSpaceRadius, CircleSides, character.Color);
    }
}