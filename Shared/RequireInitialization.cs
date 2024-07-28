namespace Shared;

public sealed class RequireInitialization<T>(bool initializeOnce = true)
{
    private T? _value;
    
    public T Value
    {
        get
        {
            if (_value is null)
            {
                throw new InvalidOperationException("Value has not been initialized.");
            }
            
            return _value;
        }
        set
        {
            if (_value is not null && initializeOnce)
            {
                throw new InvalidOperationException("Value has already been initialized.");
            }
            
            _value = value;
        }
    }
    
}