using Pong.Core;

using var game = new DIGame(services => services.RegisterServices())
{
    ContentRootDirectory = string.Empty
};

game.Run();