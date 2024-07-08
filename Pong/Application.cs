using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Models;
using Pong.Services;

namespace Pong;

public class Application : Game
{
    private readonly RequireInitialization<GraphicsDeviceManager> _graphics = new();
    private readonly RequireInitialization<SpriteBatch> _spriteBatch = new();
    
    private readonly IServiceCollection _serviceCollection = new ServiceCollection();
    private readonly RequireInitialization<IServiceProvider> _serviceProvider = new();
    private IServiceProvider ServiceProvider => _serviceProvider.Value;

    public Application()
    {
        _graphics.Value = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _serviceCollection.AddSingleton(GraphicsDevice);
        
        var gameProperties = new GameProperties();
        _serviceCollection.AddSingleton(gameProperties);
        
        gameProperties.ScreenHeight = GraphicsDevice.Viewport.Height;
        gameProperties.ScreenWidth = GraphicsDevice.Viewport.Width;
        
        _serviceCollection.AddSingleton<GameState>();

        _serviceCollection.AddSingleton<GameDrawerService>();
        _serviceCollection.AddSingleton<GameLogicService>();
        _serviceCollection.AddSingleton<GameInputService>();
        
        _serviceProvider.Value = _serviceCollection.BuildServiceProvider();
        
        base.Initialize();
        
        var gameDrawerService = ServiceProvider.GetRequiredService<GameDrawerService>();
        gameDrawerService.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch.Value = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
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
}