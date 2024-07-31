using Entombed.Code.Game.Gui;
using Entombed.Code.Game.Navigation;
using Microsoft.Xna.Framework.Graphics;
using Shared.Lifetime;

namespace Entombed.Code.Game;

public class GameGuiService(GraphicsDevice graphicsDevice, MetricsGuiService metricsGuiService, NavigationGuiService navigationGuiService) : IGuiService
{
    private readonly SpriteBatch _spriteBatch = new(graphicsDevice);

    public void DrawGui()
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        metricsGuiService.DrawGui(_spriteBatch);
        navigationGuiService.DrawGui(_spriteBatch);
        
        _spriteBatch.End();
    }
}