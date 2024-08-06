using Shared.Input.Keyboard;
using Shared.Lifetime;

namespace Entombed.Game.Menu;

public class MenuInputService(
    Keyboard keyboard,
    GameInputScheme gameInputScheme,
    MenuInput menuInput) : IInputService
{
    public void Input()
    {
        menuInput.ToggleShow = keyboard.Get(gameInputScheme.MenuKey).Pressed;

        menuInput.Up = keyboard.Get(gameInputScheme.MenuUp).Pressed;
        menuInput.Down = keyboard.Get(gameInputScheme.MenuDown).Pressed;
        
        menuInput.Select = keyboard.Get(gameInputScheme.MenuSelect).Pressed;
    }
}