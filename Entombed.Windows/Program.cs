using Entombed;
using Entombed.Game;
using Shared;

using var game = new DIGame(s => s.RegisterServices(), new GameScene())
{
    ContentRootDirectory = "Content"
};

game.IsFixedTimeStep = true;
game.IsMouseVisible = true;

game.Run();