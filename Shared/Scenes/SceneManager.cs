namespace Shared.Scenes;

public class SceneManager(Scene initialScene)
{
    internal Scene Current { get; set; } = initialScene;
    public Id<Scene> CurrentId => Current.Id;
    
    internal Scene? Next { get; set; }

    public void Transition<T>() where T : Scene, new()
    {
        Next = new T();
    }
}