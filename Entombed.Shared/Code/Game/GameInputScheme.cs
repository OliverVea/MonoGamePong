using Microsoft.Xna.Framework.Input;

namespace Entombed.Code.Game;

public class GameInputScheme
{
    // Player
    public Keys MoveUpKey { get; set; } = Keys.W;
    public Keys MoveDownKey { get; set; } = Keys.S;
    public Keys MoveLeftKey { get; set; } = Keys.A;
    public Keys MoveRightKey { get; set; } = Keys.D;
    
    public Keys UseKey { get; set; } = Keys.Space;
    public Keys AttackKey { get; set; } = Keys.E;
    
    
    // Camera
    public Keys ZoomInKey { get; set; } = Keys.Z;
    public Keys ZoomOutKey { get; set; } = Keys.X;
}