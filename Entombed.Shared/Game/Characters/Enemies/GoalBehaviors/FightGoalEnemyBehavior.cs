using Entombed.Game.Levels;
using Microsoft.Extensions.Logging;
using Shared.Metrics;

namespace Entombed.Game.Characters.Enemies.GoalBehaviors;

public class FightGoalEnemyBehavior(Level level, ILogger<FightPlayerEnemyGoalBehavior> logger, TimeMetrics timeMetrics, RoomLookup roomLookup) : EnemyGoalBehavior(timeMetrics, roomLookup)
{
    public override EnemyGoal Goal => EnemyGoal.FightGoal;
    
    public override OneOf<Success, Skipped> Update(Enemy enemy)
    {
        var goalRoom = GetPositionRoom(level.Goal);
        if (goalRoom.IsT1)
        {
            logger.LogError("Goal is not in a room");
            return new Skipped();
        }
        
        var enemyRoom = GetCharacterRoom(enemy);
        if (enemyRoom.IsT1)
        {
            logger.LogWarning("Enemy ({EnemyId}) is not in a room", enemy.Id);
            return new Skipped();
        }
        
        if (enemyRoom.AsT0 != goalRoom.AsT0) return new Skipped();
        
        var delta = level.Goal - enemy.Position;
        var distance = delta.Length();

        if (distance > enemy.AttackRange) MoveTowards(enemy, level.Goal, enemy.Speed);
        else AttackGoal(enemy);
        
        return new Success();
    }
}