using System;
using System.Collections.Generic;
using System.Linq;
using Entombed.Game.Characters.Players;
using Entombed.Game.Levels;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Camera;
using Vector2 = System.Numerics.Vector2;

namespace Entombed.Game.Characters;

public class CharacterDrawService(Player player, CharacterLookup characterLookup, IsometricCamera isometricCamera, RoomLookup roomLookup, ILogger<CharacterDrawService> logger)
{
    private const int CircleSides = 32;
    private static readonly TimeSpan HitEffectDuration = TimeSpan.FromMilliseconds(250);
    private static readonly Color HitEffectColor = Color.Red;

    public void Draw(SpriteBatch spriteBatch)
    {
        var litRoomIds = GetLitRoomIds();
        
        foreach (var character in characterLookup.Values)
        {
            DrawCharacter(spriteBatch, character, litRoomIds);
        }
    }

    private void DrawCharacter(SpriteBatch spriteBatch, Character character, IReadOnlySet<Id<Room>> litRoomIds)
    {
        if (!ShouldDrawCharacter(character, litRoomIds)) return;
        
        var screenSpacePosition = isometricCamera.WorldToScreen(character.Position);
        var screenSpaceRadius = isometricCamera.WorldToScreen(character.Radius);

        var wasAttacked = DateTime.Now - character.AttackedTime < HitEffectDuration;
        
        var characterColor = wasAttacked ? Color.Lerp(character.Color, HitEffectColor, 0.7f) : character.Color;
        
        spriteBatch.DrawCircle(screenSpacePosition, screenSpaceRadius, CircleSides, characterColor);

        var direction = new Vector2(character.Direction.X, -character.Direction.Y);
        var screenSpaceDirection = screenSpacePosition + direction * screenSpaceRadius;
        spriteBatch.DrawLine(screenSpacePosition, screenSpaceDirection, characterColor);
    }
    
    private bool ShouldDrawCharacter(Character character, IReadOnlySet<Id<Room>> litRoomIds)
    {
        var characterRoomResult = roomLookup.GetRoomWithPosition(character.Position);

        if (characterRoomResult.IsT1)
        {
            logger.LogWarning("Character {CharacterId} is not in a room", character.Id);
            return false;
        }
        
        var characterRoom = roomLookup.Get(characterRoomResult.AsT0);
        return characterRoom.Revealed && litRoomIds.Contains(characterRoom.Id);
    }
    
    private HashSet<Id<Room>> GetLitRoomIds()
    {
        var litRoomIds = roomLookup.Values.Where(x => x.Lit).Select(x => x.Id).ToHashSet();
        
        var playerRoom = roomLookup.GetRoomWithPosition(player.Position);
        if (playerRoom.IsT0) litRoomIds.Add(playerRoom.AsT0);
        
        return litRoomIds;
    }
}