namespace Shared.Events;

public interface IEventInvoker<in T>
{
    void Invoke(T value);
}