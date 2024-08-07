using Entombed.Game.Levels;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Shared.Metrics;
using Shared.Random;

namespace Entombed.Game.Characters.Enemies.GoalBehaviors;

public class IdleEnemyGoalBehavior(TimeMetrics timeMetrics, ILogger<IdleEnemyGoalBehavior> logger, RoomLookup roomLookup) : EnemyGoalBehavior(timeMetrics, roomLookup)
{
    private const float Padding = 0.1f;
    private const float PaddingScale = 1 + Padding;
    
    public override EnemyGoal Goal => EnemyGoal.Idle;
    
    public override OneOf<Success, Skipped> Update(Enemy enemy)
    {
        if (enemy.IdleTarget is { } idleTarget)
        {
            MoveTowards(enemy, idleTarget, enemy.IdleSpeed);
            
            var idleDistance = Vector2.DistanceSquared(enemy.Position, idleTarget);
            if (idleDistance < enemy.IdlePrecision * enemy.IdlePrecision)
            {
                enemy.IdleTarget = null;
                enemy.IdleTime = RandomHelper.RandomFloat(enemy.IdlePatienceFrom, enemy.IdlePatienceTo);
            }
            
            return new Success();
        }
        
        enemy.IdleTime -= TimeMetrics.DeltaTime;
        
        if (enemy.IdleTime <= 0)
        {
            var roomResult = GetCharacterRoom(enemy);
            if (roomResult.IsT1)
            {
                logger.LogWarning("Enemy ({EnemyId}) is not in a room", enemy.Id);
                return new Skipped();
            }
            
            var roomId = roomResult.AsT0;
            var room = RoomLookup.Get(roomId);

            var area = room.Areas.PickOne();
            enemy.IdleTarget = area.SamplePoint(padding: enemy.Radius * PaddingScale);
            
            return new Success();
        }

        return new Success();
    }
}