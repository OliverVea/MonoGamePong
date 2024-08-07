﻿using Entombed;
using Entombed.Loading;
using Shared;

using var game = new DIGame(s => s.RegisterServices(), new LoadingScene())
{
    ContentRootDirectory = "Content"
};

game.IsFixedTimeStep = true;
game.IsMouseVisible = true;

game.Run();