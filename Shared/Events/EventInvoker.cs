namespace Shared.Events;

internal sealed class EventInvoker<T>(Event<T> sourceEvent) : IEventInvoker<T>
{
    public void Invoke(T value)
    {
        sourceEvent.OnEvent?.Invoke(value);
    }
}