using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace Shared.Events;

internal sealed class EventInvoker<T>(BaseEvent<T> sourceBaseEvent, ILogger<T> logger) : IEventInvoker<T>
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };
    
    public void Invoke(T value)
    {
        sourceBaseEvent.OnEvent?.Invoke(value);
        
        logger.LogInformation("{Event} invoked", value);
        logger.LogDebug("{Event} invoked with {Values}", value, JsonSerializer.Serialize(value, _jsonSerializerOptions));
    }
}