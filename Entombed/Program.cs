using Entombed;
using Entombed.Code.Game;
using Entombed.Code.MainMenu;

using var game = new DIGame(s => s.RegisterServices(), new GameScene())
{
    ContentRootDirectory = "Content"
};

game.IsFixedTimeStep = true;
game.IsMouseVisible = true;

game.Run();