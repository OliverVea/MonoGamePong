using DITemplate;

using var game = new DIGame(s => s.RegisterServices())
{
    ContentRootDirectory = "Content"
};

game.IsMouseVisible = true;

game.Run();