using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shared.Content;

namespace Entombed.Code;

public class ContentLoader(ContentManager contentManager) : 
    IContentLoader<SpriteFont>
    //IContentLoader<Texture2D>,
    //IContentLoader<SoundEffect>,
    //IContentLoader<Effect>
{

    public IEnumerable<(Id<SpriteFont> ContentId, SpriteFont Content)> Load()
    {
        var font = contentManager.Load<SpriteFont>("arial");
        
        yield return (Ids.Arial, font);
    }
}