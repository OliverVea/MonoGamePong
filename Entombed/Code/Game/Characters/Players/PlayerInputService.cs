using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Shared.Lifetime;
using Keyboard = Shared.Input.Keyboard.Keyboard;

namespace Entombed.Code.Game.Characters.Players;

public class PlayerInputService(PlayerInput input, Keyboard keyboard, PlayerInputScheme inputScheme) : IInputService
{
    public void Input()
    {
        var movement = Vector2.Zero;

        if (GetDown(inputScheme.MoveUpKey)) movement.Y += 1;
        if (GetDown(inputScheme.MoveDownKey)) movement.Y -= 1;
        if (GetDown(inputScheme.MoveRightKey)) movement.X += 1;
        if (GetDown(inputScheme.MoveLeftKey)) movement.X -= 1;
        
        if (movement != Vector2.Zero) movement.Normalize();
        
        input.Movement = movement;
        
        if (GetPressed(inputScheme.UseKey)) input.Use = true;
    }
    
    private bool GetDown(Keys key) => keyboard.Get(key).Down;
    private bool GetPressed(Keys key) => keyboard.Get(key).Pressed;
}