using Microsoft.Xna.Framework;

namespace Entombed.Code.Game.Characters;

public class Player
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public float Radius { get; set; } = 0.15f;
    public float Speed => 5f;
}