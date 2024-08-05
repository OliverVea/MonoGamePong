using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Content;
using Shared.Metrics;

namespace Entombed.Game.Gui;

public class MetricsGuiService(ContentLookup<SpriteFont> fontLookup, TimeMetrics timeMetrics, GuiState guiState)
{
    private readonly SpriteFont _font = fontLookup.Get(Ids.Arial);
    
    public void DrawGui(SpriteBatch spriteBatch)
    {
        if (!guiState.ShowMetrics) return;
        
        var fps = $"fps - {timeMetrics.FrameRate:F1}";
        var tps = $"tps - {timeMetrics.TickRate:F1}";
        
        spriteBatch.DrawString(_font, fps, new Vector2(10, 10), Color.White);
        spriteBatch.DrawString(_font, tps, new Vector2(10, 30), Color.White);
    }
}