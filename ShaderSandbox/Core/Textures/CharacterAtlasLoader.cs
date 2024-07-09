using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShaderSandbox.Core.Textures;

public class CharacterAtlasLoader(ContentManager contentManager) : IContentLoader<TextureAtlas>
{
    private const string TexturePath = "sprites/character_adventurer";
    private const int SpriteWidth = 50;
    private const int SpriteHeight = 37;

    public (Id<TextureAtlas> ContentId, TextureAtlas Content) Load()
    {
        var texture = contentManager.Load<Texture2D>(TexturePath);
        
        var textureAtlas = new TextureAtlas
        {
            Texture = texture,
            SpriteWidth = SpriteWidth,
            SpriteHeight = SpriteHeight
        };

        return (TextureIds.CharacterAtlas, textureAtlas);
    }
}