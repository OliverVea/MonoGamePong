﻿using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;
using Shared.Scenes;

namespace Entombed.MainMenu;

public class MainMenuScene : Scene
{
    public override void RegisterServices(IServiceCollection serviceCollection)
    {
        serviceCollection.RegisterSelfAndInterfaces<MainMenuService>();
    }
}