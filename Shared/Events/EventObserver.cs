namespace Shared.Events;

internal sealed class EventObserver<T>(BaseEvent<T> sourceBaseEvent) : IDisposable, IEventObserver<T>
{
    private readonly List<Action<T>> _actions = [];

    public void Subscribe(Action<T> action)
    {
        _actions.Add(action);
        sourceBaseEvent.OnEvent += action;
    }
    
    public void Unsubscribe(Action<T> action)
    {
        _actions.Remove(action);
        sourceBaseEvent.OnEvent -= action;
    }
    
    public void Dispose()
    {
        foreach (var action in _actions)
        {
            sourceBaseEvent.OnEvent -= action;
        }
    }
}