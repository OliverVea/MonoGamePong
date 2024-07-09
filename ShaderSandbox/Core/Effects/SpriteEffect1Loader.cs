using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShaderSandbox.Core.Textures;

namespace ShaderSandbox.Core.Effects;

public class SpriteEffect1Loader(ContentManager contentManager) : IContentLoader<Effect>
{
    private const string SpriteEffectPath = "shaders/sprite_effect_1";

    public (Id<Effect> ContentId, Effect Content) Load()
    {
        var spriteEffect = contentManager.Load<Effect>(SpriteEffectPath);

        return (EffectIds.SpriteEffect1, spriteEffect);
    }
}