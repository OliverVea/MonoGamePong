using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shared.Random;

namespace Entombed.Code.Game.Characters.Enemies;

public class Enemy : Character
{
    public override Color Color => Color.White;
    public IReadOnlyList<EnemyGoal> Strategy { get; } = [
        EnemyGoal.FightPlayer,
        EnemyGoal.Idle
    ];
    
    public float IdlePatienceFrom => 5f;
    public float IdlePatienceTo => 10f;
    public float IdleTime { get; set; } = RandomHelper.RandomFloat(5, 10);
    public float IdlePrecision => 0.1f;
    public Vector2? IdleTarget { get; set; }
    public float IdleSpeed => 1f;
    public override float Speed => 3f;
}