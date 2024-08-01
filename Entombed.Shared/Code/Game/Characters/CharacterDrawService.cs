using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Camera;
using Vector2 = System.Numerics.Vector2;

namespace Entombed.Code.Game.Characters;

public class CharacterDrawService(CharacterLookup characterLookup, IsometricCamera isometricCamera)
{
    private const int CircleSides = 32;
    private static readonly TimeSpan HitEffectDuration = TimeSpan.FromMilliseconds(250);
    private static readonly Color HitEffectColor = Color.Red;

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

        var wasAttacked = DateTime.Now - character.AttackedTime < HitEffectDuration;
        
        var characterColor = wasAttacked ? Color.Lerp(character.Color, HitEffectColor, 0.7f) : character.Color;
        
        spriteBatch.DrawCircle(screenSpacePosition, screenSpaceRadius, CircleSides, characterColor);

        var direction = new Vector2(character.Direction.X, -character.Direction.Y);
        var screenSpaceDirection = screenSpacePosition + direction * screenSpaceRadius;
        spriteBatch.DrawLine(screenSpacePosition, screenSpaceDirection, characterColor);
    }
}