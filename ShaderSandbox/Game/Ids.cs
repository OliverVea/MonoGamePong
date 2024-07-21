using Microsoft.Xna.Framework.Graphics;
using Shared.Sprites;

namespace ShaderSandbox.Game;

public static class Ids
{
    public static readonly Id<TextureAtlas> CharacterAtlas = Id<TextureAtlas>.NewId();
    
    public static readonly Id<Effect> SpriteEffect1 = Id<Effect>.NewId();
    
    public static readonly Id<CharacterSprite> PlayerCharacter = Id<CharacterSprite>.NewId();
}