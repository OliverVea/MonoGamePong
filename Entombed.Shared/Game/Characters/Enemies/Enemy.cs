using System.Collections.Generic;
using Entombed.Game.Levels;
using Microsoft.Xna.Framework;
using Shared.Navigation;
using Shared.Random;

namespace Entombed.Game.Characters.Enemies;

public class Enemy : Character
{
    public override Color Color => Color.White;
    public IReadOnlyList<EnemyGoal> Strategy { get; } = [
        EnemyGoal.FightPlayer,
        EnemyGoal.FightGoal,
        EnemyGoal.ChaseGoal,
        EnemyGoal.Idle
    ];
    
    public float IdlePatienceFrom => 0f;
    public float IdlePatienceTo => 6f;
    public float IdleTime { get; set; } = RandomHelper.RandomFloat(5, 10);
    public float IdlePrecision => 0.1f;
    public Vector2? IdleTarget { get; set; }
    public float IdleSpeed => Speed * 0.66f;
    private const float EnemySpeedDeviation = 0.1f;
    private static float SpeedDeviation => RandomHelper.Normal(EnemySpeedDeviation);
    public override float Speed { get; } = 1.5f + SpeedDeviation;
    public EnemyGoal LastGoal { get; set; } = EnemyGoal.None;
    
    public EnemyNavigationState? NavigationState { get; set; }
}

public class EnemyNavigationState
{
    public required NavigationPath Path { get; set; }
    public int PathIndex { get; set; } = 0;
    public Id<Room> GoalRoomId { get; set; } = Id<Room>.Empty;
}