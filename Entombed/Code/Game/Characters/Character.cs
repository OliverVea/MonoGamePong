using Microsoft.Xna.Framework;

namespace Entombed.Code.Game.Characters;

public abstract class Character
{
    public Id<Character> Id { get; } = Id<Character>.NewId();
    public Vector2 Position { get; set; } = Vector2.Zero;
    public virtual float Radius { get; } = 0.15f;
    public abstract Color Color { get; }
    public float Speed => 5f;
}