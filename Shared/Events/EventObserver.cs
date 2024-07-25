namespace Shared.Events;

internal sealed class EventObserver<T>(Event<T> sourceEvent) : IDisposable, IEventObserver<T>
{
    private readonly List<Action<T>> _actions = [];

    public void Subscribe(Action<T> action)
    {
        _actions.Add(action);
        sourceEvent.OnEvent += action;
    }
    
    public void Unsubscribe(Action<T> action)
    {
        _actions.Remove(action);
        sourceEvent.OnEvent -= action;
    }
    
    public void Dispose()
    {
        foreach (var action in _actions)
        {
            sourceEvent.OnEvent -= action;
        }
    }
}