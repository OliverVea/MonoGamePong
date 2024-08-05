using System.Linq;
using Entombed.Game.Levels;
using Microsoft.Xna.Framework;
using Shared.Metrics;

namespace Entombed.Game.Characters.Enemies;

public abstract class EnemyGoalBehavior(TimeMetrics timeMetrics, RoomLookup roomLookup)
{
    protected TimeMetrics TimeMetrics { get; } = timeMetrics;
    protected RoomLookup RoomLookup { get; } = roomLookup;
    
    public abstract EnemyGoal Goal { get; }
    
    public abstract OneOf<Success, Skipped> Update(Enemy enemy);
    
    protected void MoveTowards(Enemy enemy, Vector2 position, float speed)
    {
        var delta = position - enemy.Position;
        var direction = Vector2.Normalize(delta);

        enemy.Position += direction * speed * TimeMetrics.DeltaTime;
        enemy.Direction = direction;
    }

    protected OneOf<Id<Room>, NotFound> GetCharacterRoom(Character character)
    {
        return GetPositionRoom(character.Position);
    }

    protected OneOf<Id<Room>, NotFound> GetPositionRoom(Vector2 position)
    {
        var room = RoomLookup.Values.FirstOrDefault(x => x.Areas.Any(a => a.Contains(position)));
        if (room == null) return new NotFound();
        
        return room.Id;
    }
    
    protected void AttackPlayer(Enemy enemy)
    {
    }

    protected void AttackGoal(Enemy enemy)
    {
        
    }
}