using Microsoft.Xna.Framework.Input;

namespace Entombed.Code.Game.Characters.Players;

public class PlayerInputScheme
{
    public Keys MoveUpKey { get; set; } = Keys.W;
    public Keys MoveDownKey { get; set; } = Keys.S;
    public Keys MoveLeftKey { get; set; } = Keys.A;
    public Keys MoveRightKey { get; set; } = Keys.D;
    
    public Keys UseKey { get; set; } = Keys.Space;
}