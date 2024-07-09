using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Core;
using Pong.Core.Models;
using Pong.Core.Services;

namespace Pong;

public class PongGame : Game
{
    private readonly RequireInitialization<GraphicsDeviceManager> _graphics = new();
    private readonly RequireInitialization<SpriteBatch> _spriteBatch = new();

    private readonly RequireInitialization<IServiceScope> _serviceScope = new(initializeOnce: false);
    private readonly RequireInitialization<IServiceProvider> _serviceProvider = new();
    private IServiceProvider ServiceProvider => _serviceScope.Value.ServiceProvider;

    public PongGame()
    {
        _graphics.Value = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.RegisterServices();
        
        serviceCollection.AddSingleton(GraphicsDevice);
        
        var gameProperties = new GameProperties();
        serviceCollection.AddSingleton(gameProperties);
        
        gameProperties.ScreenHeight = GraphicsDevice.Viewport.Height;
        gameProperties.ScreenWidth = GraphicsDevice.Viewport.Width;
        
        _serviceProvider.Value = serviceCollection.BuildServiceProvider();
        CreateScope();
        
        base.Initialize();

        var initializables = ServiceProvider.GetServices<Initializable>().OrderByDescending(i => i.InitializationPriority);
        foreach (var initializable in initializables) initializable.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch.Value = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        CreateScope();
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        var gameInputService = ServiceProvider.GetRequiredService<GameInputService>();
        var gameInput = gameInputService.GetGameInput(gameTime);
        
        var gameLogicService = ServiceProvider.GetRequiredService<GameLogicService>();
        gameLogicService.Update(gameInput);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        _spriteBatch.Value.Begin();
        
        var gameDrawerService = ServiceProvider.GetRequiredService<GameDrawerService>();
        gameDrawerService.Draw(_spriteBatch.Value);
        
        _spriteBatch.Value.End();

        base.Draw(gameTime);
    }

    private void CreateScope() => _serviceScope.Value = _serviceProvider.Value.CreateScope();
}