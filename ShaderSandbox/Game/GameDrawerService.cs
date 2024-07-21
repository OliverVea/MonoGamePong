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
    SpriteBatch spriteBatch,
    ContentLookup<CharacterSprite> characterSpriteLookup,
    ContentLookup<Effect> effectLookup) : IDrawService
{
    public void Draw()
    {
        var playerSprite = characterSpriteLookup.Get(Ids.PlayerCharacter);
        var effect = effectLookup.Get(Ids.SpriteEffect1);
        
        effect.Parameters["mousePosition"].SetValue(mouse.Position);

        var samplerState = new SamplerState
        {
            Filter = TextureFilter.Point
        };
        
        spriteBatch.Begin(effect: effect, samplerState: samplerState);

        var elapsedSeconds = (float)gameTime.TotalGameTime.TotalSeconds;

        var (texture, rectangle) = playerSprite.Current.GetTexture(elapsedSeconds);
        var position = new Vector2(100, 100);
        
        spriteBatch.Draw(texture, position, rectangle, Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        
        spriteBatch.End();
    }
}