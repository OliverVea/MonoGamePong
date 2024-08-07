using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Extensions;
using Shared.Lifetime;
using Shared.Options;
using Shared.Scenes;

namespace Shared;

public class DIGame : Game
{
    private readonly Action<IServiceCollection>? _configureGlobalServices;
    private readonly SceneManager _sceneManager;
    
    private readonly RequireInitialization<GraphicsDeviceManager> _graphics = new();
    
    private readonly IServiceCollection _globalServiceCollection = new ServiceCollection();
    private readonly RequireInitialization<ServiceProvider> _serviceProvider = new(initializeOnce: false);
    private readonly RequireInitialization<IServiceScope> _serviceScope = new(initializeOnce: false);
    private readonly RequireInitialization<ILogger<DIGame>> _logger = new();
    
    public required string ContentRootDirectory
    {
        get => Content.RootDirectory;
        init => Content.RootDirectory = value;
    }
    
    public required string? ConfigurationFilePath { get; init; }

    public DIGame(Action<IServiceCollection>? configureGlobalServices, Scene? initialScene = null)
    {
        _graphics.Value = new GraphicsDeviceManager(this);
        
        _configureGlobalServices = configureGlobalServices;
        _sceneManager = new SceneManager(initialScene ?? Scene.Empty);

        _graphics.Value.GraphicsProfile = GraphicsProfile.HiDef;
    }

    protected override void Initialize()
    {
        _globalServiceCollection.AddSingleton(_ => Content);
        _globalServiceCollection.AddSingleton(_ => GraphicsDevice);
        _globalServiceCollection.AddSingleton(_ => new GameConfiguration(this));
        _globalServiceCollection.AddSingleton(_ => new GameTime());
        _globalServiceCollection.AddSingleton(_ => _sceneManager);

        var configurationBuilder = new ConfigurationBuilder();
        
        if (ConfigurationFilePath is not null) configurationBuilder.AddIniFile(ConfigurationFilePath);
        
        var configuration = configurationBuilder.Build();
        _globalServiceCollection.AddSingleton<IConfiguration>(_ => configuration);
        
        _globalServiceCollection.AddLogging(c =>
        {
            c.AddConfiguration(configuration.GetSection("Logging"));
            c.AddSimpleConsole(o => { o.SingleLine = true; });
        });
        
        _globalServiceCollection.AddOptions();
        _globalServiceCollection.AddOptions<DisplayOptions>().BindConfiguration(DisplayOptions.SectionName);
        
        _configureGlobalServices?.Invoke(_globalServiceCollection);

        _serviceProvider.Value = BuildServiceProvider();
        
        _logger.Value = _serviceProvider.Value.GetRequiredService<ILogger<DIGame>>();
        
        var displayOptions = _serviceProvider.Value.GetRequiredService<IOptions<DisplayOptions>>().Value;
        
        _graphics.Value.IsFullScreen = displayOptions.Fullscreen;
        _graphics.Value.PreferredBackBufferWidth = displayOptions.Width;
        _graphics.Value.PreferredBackBufferHeight = displayOptions.Height;
        _graphics.Value.ApplyChanges();
        
        base.Initialize();
    }

    private ServiceProvider BuildServiceProvider()
    {
        var serviceCollection = new ServiceCollection { _globalServiceCollection };
        
        var scene = _sceneManager.Current;

        scene.RegisterServices(serviceCollection);
        
        return serviceCollection.BuildServiceProvider();
    }

    protected override void LoadContent()
    {
        SceneStartup();
        
        base.LoadContent();
    }

    private void SceneStartup()
    {
        _logger.Value.LogInformation("{SceneType} startup", _sceneManager.Current.GetType().Name);
        
        var startups = _serviceProvider.Value
            .GetServices<IStartupService>()
            .Where(x => x.Active)
            .OrderByDescending(x => x.StartupPriority);
        
        foreach (var startup in startups) startup.Startup();
    }

    protected override void Update(GameTime gameTime)
    {
        var singletonGameTime = _serviceProvider.Value.GetRequiredService<GameTime>();
        singletonGameTime.Copy(gameTime);
        
        if (_sceneManager.Next is {} nextScene) TransitionScene(nextScene);
        
        _serviceScope.Value = _serviceProvider.Value.CreateScope();
        
        var inputs = _serviceScope.Value.ServiceProvider.GetServices<IInputService>()
            .Where(x => x.Active)
            .OrderByDescending(x => x.InputPriority);
        
        foreach (var input in inputs) input.Input();
        
        var gameSystems = _serviceScope.Value.ServiceProvider.GetServices<IUpdateService>()
            .Where(x => x.Active)
            .OrderByDescending(x => x.UpdatePriority);
        
        foreach (var gameSystem in gameSystems) gameSystem.Update();

        base.Update(gameTime);
    }

    private void TransitionScene(Scene nextScene)
    {
        _sceneManager.Current = nextScene;
        _sceneManager.Next = null;
        
        _serviceScope.Value.Dispose();
        _serviceProvider.Value = BuildServiceProvider();
        
        SceneStartup();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        var drawables = _serviceScope.Value.ServiceProvider.GetServices<IDrawService>()
            .Where(x => x.Active)
            .OrderByDescending(x => x.DrawPriority);
        
        foreach (var drawable in drawables) drawable.Draw();

        base.Draw(gameTime);
        
        var guiServices = _serviceScope.Value.ServiceProvider
            .GetServices<IGuiService>()
            .Where(x => x.Active)
            .OrderByDescending(x => x.GuiPriority);
        
        foreach (var guiService in guiServices) guiService.DrawGui();
    }
    
}