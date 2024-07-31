using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Shared.Lifetime;
using Keyboard = Shared.Input.Keyboard.Keyboard;

namespace Entombed.Code.Game.Characters.Players;

public class PlayerInputService(GameInputScheme inputScheme, PlayerInput input, Keyboard keyboard) : IInputService
{
    public void Input()
    {
        input.Movement = GetVector2FromInput(
            inputScheme.MoveUpKey,
            inputScheme.MoveDownKey,
            inputScheme.MoveRightKey,
            inputScheme.MoveLeftKey);
        
        input.Use = GetPressed(inputScheme.UseKey);
        input.Attack = GetPressed(inputScheme.AttackKey);
    }
    
    private Vector2 GetVector2FromInput(Keys up, Keys down, Keys right, Keys left, bool normalize = true)
    {
        var movement = Vector2.Zero;

        if (GetDown(up)) movement.Y += 1;
        if (GetDown(down)) movement.Y -= 1;
        if (GetDown(right)) movement.X += 1;
        if (GetDown(left)) movement.X -= 1;
        
        if (normalize && movement != Vector2.Zero) movement.Normalize();
        
        return movement;
    }
    
    private bool GetDown(Keys key) => keyboard.Get(key).Down;
    private bool GetPressed(Keys key) => keyboard.Get(key).Pressed;
}