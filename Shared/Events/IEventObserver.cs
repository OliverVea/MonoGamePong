namespace Shared.Events;

public interface IEventObserver<out T>
{
    void Subscribe(Action<T> action);
    void Unsubscribe(Action<T> action);
}