using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Content;
using Shared.Lifetime;

namespace Entombed.Loading;

public class LoadingDrawService(GraphicsDevice graphicsDevice, LoadingState loadingState, ContentLookup<SpriteFont> spriteFontLookup) : IDrawService
{
    private readonly SpriteBatch _spriteBatch = new(graphicsDevice);

    public void Draw()
    {
        var font = spriteFontLookup.Get(Ids.Arial);
        var text = loadingState.LoadingText;
        
        _spriteBatch.Begin();
        
        _spriteBatch.DrawString(font, text, new Vector2(10, 10), Color.White);
        
        _spriteBatch.End();
    }
}