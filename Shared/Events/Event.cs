namespace Shared.Events;

internal sealed class Event<T>
{
    public Action<T>? OnEvent { get; set; }
}