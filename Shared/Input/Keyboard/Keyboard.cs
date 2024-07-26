using Microsoft.Xna.Framework.Input;

namespace Shared.Input.Keyboard;

public class Keyboard : IDisposable
{
    private readonly Dictionary<Keys, ButtonState> _keyStates = new();
    public IReadOnlyDictionary<Keys, ButtonState> KeyStates => _keyStates;

    public ButtonState Get(Keys key)
    {
        if (_keyStates.TryGetValue(key, out var value))
        {
            return value;
        }
        
        value = new ButtonState();
        _keyStates[key] = value;

        return value;
    }

    void IDisposable.Dispose()
    {
        Console.WriteLine("Disposing Keyboard");
    }
}