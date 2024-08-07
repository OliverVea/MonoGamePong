using Entombed.Game.Levels;
using Entombed.Game.Navigation;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Shared.Metrics;

namespace Entombed.Game.Characters.Enemies.GoalBehaviors;

public class ChaseGoalBehavior(Level level, TimeMetrics timeMetrics, RoomLookup roomLookup, ILogger<ChaseGoalBehavior> logger, NavigationState navigationState) : EnemyGoalBehavior(timeMetrics, roomLookup)
{
    public override EnemyGoal Goal => EnemyGoal.ChaseGoal;
    
    public override OneOf<Success, Skipped> Update(Enemy enemy)
    {
        var goalRoomResult = GetPositionRoom(level.Goal);
        if (goalRoomResult.IsT1)
        {
            logger.LogError("Goal is not in a room");
            return new Skipped();
        }
        var goalRoomId = goalRoomResult.AsT0;
        
        var enemyRoomResult = GetCharacterRoom(enemy);
        if (enemyRoomResult.IsT1)
        {
            logger.LogWarning("Enemy ({EnemyId}) is not in a room", enemy.Id);
            return new Skipped();
        }
        var enemyRoomId = enemyRoomResult.AsT0;
        
        if (enemyRoomId == goalRoomId) return new Skipped();
        
        var needsNewPath = enemy.NavigationState is null || enemy.NavigationState.GoalRoomId != goalRoomId || enemy.LastGoal != EnemyGoal.ChaseGoal;

        if (needsNewPath)
        {
            var navigationState = GetNewNavigationState(enemy, goalRoomId);
            if (navigationState == null)
            {
                logger.LogWarning("Could not find path to goal");
                return new Skipped();
            }
            
            enemy.NavigationState = navigationState;
        }

        return FollowPath(enemy);
    }

    private EnemyNavigationState? GetNewNavigationState(Enemy enemy, Id<Room> goalRoomId)
    {
        var enemyRoomResult = GetCharacterRoom(enemy);
        if (enemyRoomResult.IsT1)
        {
            logger.LogWarning("Enemy ({EnemyId}) is not in a room", enemy.Id);
            return null;
        }
        
        var path = navigationState.RoomPaths[(enemyRoomResult.AsT0, goalRoomId)];

        return new EnemyNavigationState
        {
            Path = path,
            GoalRoomId = goalRoomId
        };
    }

    private OneOf<Success, Skipped> FollowPath(Enemy enemy)
    {
        if (enemy.NavigationState == null) return new Skipped();
        
        var nextPosition = enemy.NavigationState.Path.Nodes[enemy.NavigationState.PathIndex].Position;
        
        MoveTowards(enemy, nextPosition, enemy.Speed);
        
        var distance = Vector2.DistanceSquared(enemy.Position, nextPosition);
        if (distance < enemy.Radius * enemy.Radius)
        {
            enemy.NavigationState.PathIndex++;
            if (enemy.NavigationState.PathIndex >= enemy.NavigationState.Path.Nodes.Length)
            {
                enemy.NavigationState = null;
                return new Success();
            }
        }

        return new Success();
    }
}