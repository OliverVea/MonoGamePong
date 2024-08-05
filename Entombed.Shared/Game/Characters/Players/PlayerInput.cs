using Microsoft.Xna.Framework;

namespace Entombed.Game.Characters.Players;

public class PlayerInput
{
    public bool Pause { get; set; } = false;
    public Vector2 Movement { get; set; } = Vector2.Zero;
    public bool Use { get; set; } = false;
    public bool Light { get; set; } = false;
    public bool Attack { get; set; } = false;
}