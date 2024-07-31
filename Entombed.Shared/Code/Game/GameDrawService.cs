using Entombed.Code.Game.Characters;
using Entombed.Code.Game.Characters.Players;
using Entombed.Code.Game.Levels;
using Microsoft.Xna.Framework.Graphics;
using Shared.Lifetime;

namespace Entombed.Code.Game;

public class GameDrawService(
    GraphicsDevice graphicsDevice,
    LevelDrawService roomDrawService,
    CharacterDrawService characterDrawService) : IDrawService
{
    private readonly SpriteBatch _spriteBatch = new(graphicsDevice);

    public void Draw()
    {
        _spriteBatch.Begin();
        
        roomDrawService.Draw(_spriteBatch);
        characterDrawService.Draw(_spriteBatch);
        
        _spriteBatch.End();
    }
}