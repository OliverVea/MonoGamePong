using Pong.Core;

using var game = new DIGame(services => services.RegisterServices())
{
    ContentRootDirectory = "Content"
};

game.Run();