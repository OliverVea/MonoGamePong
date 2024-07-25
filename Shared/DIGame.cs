using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shared.Extensions;
using Shared.Lifetime;

namespace Shared;

public class DIGame : Game
{
    private readonly Action<IServiceCollection> _registerServices;
    
    private readonly RequireInitialization<GraphicsDeviceManager> _graphics = new();
    
    private readonly RequireInitialization<IServiceScope> _serviceScope = new(initializeOnce: false);
    private readonly RequireInitialization<IServiceProvider> _serviceProvider = new();
    
    public required string ContentRootDirectory
    {
        get => Content.RootDirectory;
        init => Content.RootDirectory = value;
    }

    public DIGame(Action<IServiceCollection> registerServices)
    {
        _graphics.Value = new GraphicsDeviceManager(this);
        _registerServices = registerServices;

        _graphics.Value.GraphicsProfile = GraphicsProfile.HiDef;
    }

    protected override void Initialize()
    {
        var serviceCollection = new ServiceCollection();
        
        _registerServices.Invoke(serviceCollection);
        
        serviceCollection.AddSingleton(_ => Content);
        serviceCollection.AddSingleton(_ => GraphicsDevice);
        serviceCollection.AddSingleton(_ => new GameConfiguration(this));
        serviceCollection.AddSingleton(_ => new GameTime());
        
        _serviceProvider.Value = serviceCollection.BuildServiceProvider();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        var startups = _serviceProvider.Value
            .GetServices<IStartupService>()
            .OrderByDescending(x => x.StartupPriority);
        
        foreach (var startup in startups) startup.Startup();
    }

    protected override void Update(GameTime gameTime)
    {
        if (!IsActive) return;
        
        var singletonGameTime = _serviceProvider.Value.GetRequiredService<GameTime>();
        singletonGameTime.Copy(gameTime);
        
        _serviceScope.Value = _serviceProvider.Value.CreateScope();
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        var inputs = _serviceScope.Value.ServiceProvider.GetServices<IInputService>();
        foreach (var input in inputs) input.Input();
        
        var gameSystems = _serviceScope.Value.ServiceProvider.GetServices<IUpdateService>();
        foreach (var gameSystem in gameSystems) gameSystem.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        var drawables = _serviceScope.Value.ServiceProvider.GetServices<IDrawService>();
        foreach (var drawable in drawables) drawable.Draw();

        base.Draw(gameTime);
        
        var guiServices = _serviceScope.Value.ServiceProvider.GetServices<IGuiService>();
        foreach (var guiService in guiServices) guiService.DrawGui();
    }
    
}