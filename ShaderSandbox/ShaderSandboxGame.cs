using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShaderSandbox.Core;
using ShaderSandbox.Core.Models;
using ShaderSandbox.Core.Services;

namespace ShaderSandbox;

public class ShaderSandboxGame : Game
{
    private readonly RequireInitialization<GraphicsDeviceManager> _graphics = new();
    
    private readonly RequireInitialization<IServiceScope> _serviceScope = new(initializeOnce: false);
    private readonly RequireInitialization<IServiceProvider> _serviceProvider = new();
    private IServiceProvider ServiceProvider => _serviceScope.Value.ServiceProvider;

    private GameInput _frameGameInput = new GameInput
    {
        MousePosition = Vector2.Zero
    };

    public ShaderSandboxGame()
    {
        _graphics.Value = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.RegisterServices();

        serviceCollection.AddSingleton(new SpriteBatch(GraphicsDevice));
        serviceCollection.AddSingleton(_ => Content);
        serviceCollection.AddSingleton(_ => GraphicsDevice);
        
        var gameProperties = new GameProperties();
        serviceCollection.AddSingleton(gameProperties);
        
        _serviceProvider.Value = serviceCollection.BuildServiceProvider();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        var initializables = _serviceProvider.Value.GetServices<Initializable>().OrderByDescending(i => i.InitializationPriority);
        foreach (var initializable in initializables) initializable.Initialize();
        
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (!IsActive) return;
        
        _serviceScope.Value = _serviceProvider.Value.CreateScope();
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        var gameInputService = ServiceProvider.GetRequiredService<IGameInputService<GameInput>>();
        _frameGameInput = gameInputService.GetGameInput(gameTime);
        
        var gameLogicService = ServiceProvider.GetRequiredService<IGameLogicService<GameInput>>();
        gameLogicService.Update(_frameGameInput);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        var gameDrawerService = ServiceProvider.GetRequiredService<IGameDrawerService<GameInput>>();
        gameDrawerService.Draw(_frameGameInput);

        base.Draw(gameTime);
    }
}