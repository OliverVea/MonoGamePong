using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Shared.Lifetime;
using Keyboard = Shared.Input.Keyboard.Keyboard;

namespace Entombed.Code.Game.Characters;

public class PlayerInputService(PlayerInput playerInput, Keyboard keyboard) : IInputService
{
    private const Keys MoveUpKey = Keys.W;
    private const Keys MoveDownKey = Keys.S;
    private const Keys MoveLeftKey = Keys.A;
    private const Keys MoveRightKey = Keys.D;
    
    public void Input()
    {
        var movement = Vector2.Zero;

        if (keyboard.Get(MoveUpKey).Down) movement.Y += 1;
        if (keyboard.Get(MoveDownKey).Down) movement.Y -= 1;
        if (keyboard.Get(MoveRightKey).Down) movement.X += 1;
        if (keyboard.Get(MoveLeftKey).Down) movement.X -= 1;
        
        if (movement != Vector2.Zero) movement.Normalize();
        
        playerInput.Movement = movement;
    }
}