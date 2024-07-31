using Microsoft.Xna.Framework;

namespace Entombed.Code.Game.Characters;

public abstract class Character
{
    public float Health { get; set; } = 1f;
    
    public Id<Character> Id { get; } = Id<Character>.NewId();
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Direction { get; set; } = Vector2.Zero;
    public virtual float Radius { get; } = 0.15f;
    public float AttackRange { get; set; } = 0.5f;
    public float AttackAngle { get; set; } = MathHelper.PiOver4;
    public float AttackDamage { get; set; } = 0.1f;
    public abstract Color Color { get; }
    public float Speed => 5f;
}