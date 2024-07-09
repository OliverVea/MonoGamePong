using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShaderSandbox.Core.Effects;
using ShaderSandbox.Core.Models;
using ShaderSandbox.Core.Sprites;

namespace ShaderSandbox.Core.Services;

public class GameDrawerService(
    GameState gameState,
    SpriteBatch spriteBatch,
    ContentLookup<CharacterSprite> characterSpriteLookup,
    ContentLookup<Effect> effectLookup) : IGameDrawerService<GameInput>
{
    public void Draw(GameInput gameInput)
    {
        var playerSprite = characterSpriteLookup.Get(SpriteIds.PlayerCharacter);
        var effect = effectLookup.Get(EffectIds.SpriteEffect1);
        
        effect.Parameters["mousePosition"].SetValue(gameInput.MousePosition);

        var samplerState = new SamplerState
        {
            Filter = TextureFilter.Point
        };
        
        spriteBatch.Begin(effect: effect, samplerState: samplerState);

        var elapsedSeconds = (float)gameState.GameTime.TotalGameTime.TotalSeconds;

        var (texture, rectangle) = playerSprite.Current.GetTexture(elapsedSeconds);
        var position = new Vector2(100, 100);
        
        spriteBatch.Draw(texture, position, rectangle, Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
        
        spriteBatch.End();
    }
}