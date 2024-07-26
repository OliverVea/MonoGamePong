using Shared.Lifetime;

namespace Shared.Input.Keyboard;

public class KeyboardInputService(Keyboard keyboard) : IInputService
{
    public void Input()
    {
        var state = Microsoft.Xna.Framework.Input.Keyboard.GetState();

        foreach (var (key, buttonState) in keyboard.KeyStates)
        {
            var isKeyDown = state.IsKeyDown(key);
            
            buttonState.Pressed = isKeyDown && !buttonState.Down;
            buttonState.Released = !isKeyDown && buttonState.Down;
            
            buttonState.Down = isKeyDown;
        }
    }
}