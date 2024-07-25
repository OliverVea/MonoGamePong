using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Content;
using Shared.Lifetime;
using Shared.Screen;
using Color = Microsoft.Xna.Framework.Color;

namespace Pong.Core;

public class GuiService(GameState gameState, GraphicsDevice graphicsDevice, ContentLookup<SpriteFont> fontLookup, ContentLookup<Effect> spriteEffectLookup, Screen screen, GameInput gameInput) : IGuiService
{
    private readonly SpriteBatch _spriteBatch = new(graphicsDevice);
    private const float FontScale = 3.5f;
    
    public void DrawGui()
    {
        var spriteEffect = gameInput.UseTextShader ? spriteEffectLookup.Get(Ids.FontEffect) : null;
        
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: spriteEffect);
        
        var score = $"{gameState.LeftScore} - {gameState.RightScore}";
        var font = fontLookup.Get(Ids.Arial);
        
        var scoreSize = font.MeasureString(score) * FontScale;
        
        var positionX = screen.Width / 2f - scoreSize.X / 2f;
        var positionY = 10 + scoreSize.Y / 2f;
        
        var position = new Vector2(positionX, positionY);

        _spriteBatch.DrawString(
            font,
            score,
            position,
            Color.White, 
            0, 
            Vector2.Zero, 
            Vector2.One * FontScale,
            SpriteEffects.None, 
            0);
        
        _spriteBatch.End();
    }
}