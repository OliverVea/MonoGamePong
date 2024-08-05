using Entombed.Game.Levels;
using Shared.Metrics;

namespace Entombed.Game.Characters.Enemies.GoalBehaviors;
 
public class NoneEnemyGoalBehavior(TimeMetrics timeMetrics, RoomLookup roomLookup) : EnemyGoalBehavior(timeMetrics, roomLookup)
{
    public override EnemyGoal Goal => EnemyGoal.None;
    
    public override OneOf<Success, Skipped> Update(Enemy enemy)
    {
        return new Success();
    }
}