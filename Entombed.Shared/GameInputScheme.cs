using Microsoft.Xna.Framework.Input;

namespace Entombed;

public class GameInputScheme
{
    // Player
    public Keys MoveUpKey { get; set; } = Keys.W;
    public Keys MoveDownKey { get; set; } = Keys.S;
    public Keys MoveLeftKey { get; set; } = Keys.A;
    public Keys MoveRightKey { get; set; } = Keys.D;
    
    public Keys UseKey { get; set; } = Keys.Space;
    public Keys AttackKey { get; set; } = Keys.E;
    public Keys LightKey { get; set; } = Keys.L;
    
    public Keys PauseKey { get; set; } = Keys.P;
    
    
    // Camera
    public Keys ZoomInKey { get; set; } = Keys.Z;
    public Keys ZoomOutKey { get; set; } = Keys.X;
    
    // Menu
    public Keys MenuKey { get; set; } = Keys.Escape;
    public Keys MenuUp { get; set; } = Keys.Up;
    public Keys MenuDown { get; set; } = Keys.Down;
    public Keys MenuSelect { get; set; } = Keys.Enter;
}