﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShaderSandbox.Core.Textures;

namespace ShaderSandbox.Core.Sprites;

public class PlayerCharacterSpriteLoader(ContentManager contentManager) : IContentLoader<CharacterSprite>
{
    private const string TexturePath = "sprites/character_adventurer";
    private static readonly int[] FrameIndices = [0, 1, 2, 3];
    private const int SpriteWidth = 50;
    private const int SpriteHeight = 37;
    
    public (Id<CharacterSprite> ContentId, CharacterSprite Content) Load()
    {
        var texture = contentManager.Load<Texture2D>(TexturePath);
        
        var textureAtlas = new TextureAtlas
        {
            Texture = texture,
            SpriteWidth = SpriteWidth,
            SpriteHeight = SpriteHeight
        };

        var spriteAnimation = new SpriteAnimation
        {
            TextureAtlas = textureAtlas,
            FrameIndices = FrameIndices
        };
        
        var characterSprite = new CharacterSprite
        {
            IdleFront = spriteAnimation
        };

        return (SpriteIds.PlayerCharacter, characterSprite);
    }
}