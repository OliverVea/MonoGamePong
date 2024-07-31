namespace Shared.Events;

internal sealed class BaseEvent<T>
{
    public Action<T>? OnEvent { get; set; }
}