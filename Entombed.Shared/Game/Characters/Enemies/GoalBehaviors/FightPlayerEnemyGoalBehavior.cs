using Entombed.Game.Characters.Players;
using Entombed.Game.Levels;
using Microsoft.Extensions.Logging;
using Shared.Metrics;

namespace Entombed.Game.Characters.Enemies.GoalBehaviors;

public class FightPlayerEnemyGoalBehavior(Player player, ILogger<FightPlayerEnemyGoalBehavior> logger, TimeMetrics timeMetrics, RoomLookup roomLookup) : EnemyGoalBehavior(timeMetrics, roomLookup)
{
    public override EnemyGoal Goal => EnemyGoal.FightPlayer;
    
    public override OneOf<Success, Skipped> Update(Enemy enemy)
    {
        var playerRoom = GetCharacterRoom(player);
        if (playerRoom.IsT1)
        {
            logger.LogWarning("Player is not in a room");
            return new Skipped();
        }
        
        var enemyRoom = GetCharacterRoom(enemy);
        if (enemyRoom.IsT1)
        {
            logger.LogWarning("Enemy ({EnemyId}) is not in a room", enemy.Id);
            return new Skipped();
        }
        
        if (enemyRoom.AsT0 != playerRoom.AsT0) return new Skipped();
        
        var delta = player.Position - enemy.Position;
        var distance = delta.Length();

        if (distance > enemy.AttackRange) MoveTowards(enemy, player.Position, enemy.Speed);
        else AttackPlayer(enemy);
        
        return new Success();
    }
}