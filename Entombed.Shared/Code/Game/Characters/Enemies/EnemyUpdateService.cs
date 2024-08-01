using System;
using System.Linq;
using Entombed.Code.Game.Characters.Players;
using Entombed.Code.Game.Levels;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Shared.Lifetime;
using Shared.Metrics;
using Shared.Random;

namespace Entombed.Code.Game.Characters.Enemies;

public class EnemyUpdateService(Player player, CharacterLookup characterLookup, RoomLookup roomLookup, ILogger<EnemyUpdateService> logger, TimeMetrics timeMetrics) : IUpdateService
{
    private const float Padding = 0.1f;
    private const float PaddingScale = 1 + Padding;
    
    public void Update()
    {
        var playerRoom = GetCharacterRoom(player);
        if (playerRoom.IsT1) logger.LogWarning("Player is not in a room");
        
        var enemies = characterLookup.Values.OfType<Enemy>();
        
        foreach (var enemy in enemies)
        {
            UpdateEnemy(enemy, playerRoom);
        }
    }

    private OneOf<Id<Room>, NotFound> GetCharacterRoom(Character character)
    {
        var room = roomLookup.Values.FirstOrDefault(x => x.Areas.Any(a => a.Contains(character.Position)));
        if (room == null) return new NotFound();
        
        return room.Id;
    }

    private void UpdateEnemy(Enemy enemy, OneOf<Id<Room>, NotFound> playerRoom)
    {
        foreach (var goal in enemy.Strategy)
        {
            var goalResult = ExecuteGoal(goal, enemy, playerRoom);
            if (goalResult.IsT0) return;
        }
    }

    private OneOf<Success, Skipped> ExecuteGoal(EnemyGoal enemyGoal, Enemy enemy, OneOf<Id<Room>, NotFound> playerRoom)
    {
        return enemyGoal switch
        {
            EnemyGoal.FightPlayer => ExecuteFightPlayer(enemy, playerRoom),
            EnemyGoal.Idle => ExecuteIdle(enemy),
            EnemyGoal.Noop => new Skipped(),
            _ => throw new ArgumentOutOfRangeException(nameof(enemyGoal), enemyGoal, null)
        };
    }

    private OneOf<Success, Skipped> ExecuteFightPlayer(Enemy enemy, OneOf<Id<Room>, NotFound> playerRoom)
    {
        if (playerRoom.IsT1) return new Skipped();
        
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
        else Attack(enemy, player);
        
        return new Success();
    }

    private OneOf<Success, Skipped> ExecuteIdle(Enemy enemy)
    {
        if (enemy.IdleTarget is { } idleTarget)
        {
            MoveTowards(enemy, idleTarget, enemy.IdleSpeed);
            
            var idleDistance = Vector2.Distance(enemy.Position, idleTarget);
            if (idleDistance < enemy.IdlePrecision)
            {
                enemy.IdleTarget = null;
                enemy.IdleTime = RandomHelper.RandomFloat(enemy.IdlePatienceFrom, enemy.IdlePatienceTo);
            }
            
            return new Success();
        }
        
        enemy.IdleTime -= timeMetrics.DeltaTime;
        
        if (enemy.IdleTime <= 0)
        {
            var roomResult = GetCharacterRoom(enemy);
            if (roomResult.IsT1)
            {
                logger.LogWarning("Enemy ({EnemyId}) is not in a room", enemy.Id);
                return new Skipped();
            }
            
            var roomId = roomResult.AsT0;
            var room = roomLookup.Get(roomId);

            var area = room.Areas.PickOne();
            enemy.IdleTarget = area.SamplePoint(padding: enemy.Radius * PaddingScale);
            
            return new Success();
        }

        return new Success();
    }

    private void MoveTowards(Enemy enemy, Vector2 position, float speed)
    {
        var delta = position - enemy.Position;
        var direction = Vector2.Normalize(delta);

        enemy.Position += direction * speed * timeMetrics.DeltaTime;
        enemy.Direction = direction;
    }

    private void Attack(Enemy enemy, Character target)
    {
    }
}