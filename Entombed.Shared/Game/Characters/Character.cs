using System;
using Microsoft.Xna.Framework;
using Shared.Random;

namespace Entombed.Game.Characters;

public abstract class Character
{
    public float Health { get; set; } = 1f;
    
    public Id<Character> Id { get; } = Id<Character>.NewId();
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Direction { get; set; } = RandomHelper.RandomDirection();
    public float Radius => 0.15f;
    public float AttackRange => 0.5f;
    public float AttackAngle => MathHelper.PiOver4;
    public float AttackDamage => 0.1f;
    public DateTime AttackedTime { get; set; }
    public abstract Color Color { get; }
    public virtual float Speed => 3.5f;
}