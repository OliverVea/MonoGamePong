using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shared.Content;
using Shared.Sprites;

namespace ShaderSandbox.Game;

public class CharacterAtlasLoader(ContentManager contentManager) : IContentLoader<TextureAtlas>
{
    private const string TexturePath = "sprites/character_adventurer";
    private const int SpriteWidth = 50;
    private const int SpriteHeight = 37;

    public IEnumerable<(Id<TextureAtlas> ContentId, TextureAtlas Content)> Load()
    {
        var texture = contentManager.Load<Texture2D>(TexturePath);
        
        var textureAtlas = new TextureAtlas
        {
            Texture = texture,
            SpriteWidth = SpriteWidth,
            SpriteHeight = SpriteHeight
        };

        return [(Ids.CharacterAtlas, textureAtlas)];
    }
}