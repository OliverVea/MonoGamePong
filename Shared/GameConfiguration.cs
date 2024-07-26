using Microsoft.Xna.Framework;

namespace Shared;

public class GameConfiguration(Game game)
{
    public bool IsMouseVisible { get => game.IsMouseVisible; set => game.IsMouseVisible = value; }
    public string ContentRootDirectory { get => game.Content.RootDirectory; set => game.Content.RootDirectory = value; }
    public bool Exit
    {
        get => false;
        set
        {
            if (value)
            {
                game.Exit();
            }
        }
    }
}