using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Content;
using Shared.Lifetime;
using Shared.Mouse;
using Shared.Sprites;

namespace ShaderSandbox.Game;

public class GameDrawerService(
    Mouse mouse,
    GameTime gameTime,
    GraphicsDevice graphicsDevice,
    ContentLookup<CharacterSprite> characterSpriteLookup,
    ContentLookup<Effect> effectLookup) : IDrawService
{
    private readonly SpriteBatch _spriteBatch = new(graphicsDevice);
    
    public void Draw()
    {
        var playerSprite = characterSpriteLookup.Get(Ids.PlayerCharacter);
        var effect = effectLookup.Get(Ids.SpriteEffect1);
        
        effect.Parameters["mousePosition"].SetValue(mouse.Position);

        var samplerState = new SamplerState
        {
            Filter = TextureFilter.Point
        };
        
        _spriteBatch.Begin(effect: effect, samplerState: samplerState);

        var elapsedSeconds = (float)gameTime.TotalGameTime.TotalSeconds;

        var (texture, rectangle) = playerSprite.Current.GetTexture(elapsedSeconds);
        var position = new Vector2(100, 100);
        
        _spriteBatch.Draw(texture, position, rectangle, Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        
        _spriteBatch.End();
    }
}