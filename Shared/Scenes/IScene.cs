using Microsoft.Extensions.DependencyInjection;

namespace Shared.Scenes;

public abstract class Scene
{
    public Id<Scene> Id { get; } = Id<Scene>.NewId();
    public abstract void RegisterServices(IServiceCollection serviceCollection);

    public static Scene Empty { get; } = new EmptyScene();

    private class EmptyScene : Scene
    {
        public override void RegisterServices(IServiceCollection serviceCollection) { }
    }
}