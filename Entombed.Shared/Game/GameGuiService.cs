using Entombed.Game.Characters.Enemies;
using Entombed.Game.Gui;
using Entombed.Game.Levels;
using Entombed.Game.Navigation;
using Microsoft.Xna.Framework.Graphics;
using Shared.Lifetime;

namespace Entombed.Game;

public class GameGuiService(
    GraphicsDevice graphicsDevice,
    NavigationGuiService navigationGuiService, 
    EnemyGuiService enemyGuiService,
    LevelGuiService levelGuiService,
    MetricsGuiService metricsGuiService) : IGuiService
{
    private readonly SpriteBatch _spriteBatch = new(graphicsDevice);

    public void DrawGui()
    {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        
        metricsGuiService.DrawGui(_spriteBatch);
        navigationGuiService.DrawGui(_spriteBatch);
        enemyGuiService.DrawGui(_spriteBatch);
        levelGuiService.DrawGui(_spriteBatch);
        
        _spriteBatch.End();
    }
}