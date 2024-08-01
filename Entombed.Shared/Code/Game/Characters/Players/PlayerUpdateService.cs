using System;
using System.Linq;
using Entombed.Code.Game.Characters.Enemies;
using Entombed.Code.Game.Levels;
using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Shared.Lifetime;
using Shared.Metrics;

namespace Entombed.Code.Game.Characters.Players;

public class PlayerUpdateService(
    Player player,
    PlayerInput playerInput,
    CharacterLookup characterLookup,
    CharacterDamageService characterDamageService,
    TimeMetrics timeMetrics,
    LevelCollisionService levelCollisionService,
    DoorService doorService,
    DoorLookup doorLookup) : IUpdateService
{
    public void Update()
    {
        UpdatePlayerMovement();
        UpdatePlayerInteraction();
        UpdatePlayerAttack();
    }

    private void UpdatePlayerMovement()
    {
        var newPosition = player.Position + playerInput.Movement * player.Speed * timeMetrics.DeltaTime;
        var characterCircle = new Circle(newPosition, player.Radius);
        
        if (playerInput.Movement != Vector2.Zero)  player.Direction = Vector2.Normalize(playerInput.Movement);

        var movementCollidesWithLevel = levelCollisionService.CollidesWithLevel(characterCircle);
        if (movementCollidesWithLevel) return;
        
        player.Position = newPosition;
    }

    private void UpdatePlayerInteraction()
    {
        if (!playerInput.Use) return;
        
        var (closestDoor, distance) = doorLookup.Values
            .Select(x => (Door: x, Distance: x.LineSegment.Distance(player.Position)))
            .MinBy(x => x.Distance);
        
        if (distance > player.InteractionDistance) return;
        
        doorService.OpenDoor(closestDoor.Id);
    }

    private void UpdatePlayerAttack()
    {
        if (!playerInput.Attack) return;

        var enemies = characterLookup.Values.OfType<Enemy>().ToArray();
        var enemiesInRange = enemies.Where(x => DistanceToPlayer(x) < player.AttackRange).ToArray();
        var hitEnemies = enemiesInRange.Where(x => AngleToPlayer(x) < player.AttackAngle).ToArray();
        
        foreach (var enemy in hitEnemies) characterDamageService.Damage(player.Id, enemy.Id, player.AttackDamage);

    }
    
    private float DistanceToPlayer(Enemy enemy) => Vector2.Distance(player.Position, enemy.Position);
    private float AngleToPlayer(Enemy enemy) => Angle(player.Direction, Vector2.Normalize(enemy.Position - player.Position), false);
    private float Angle(Vector2 a, Vector2 b, bool normalize = true)
    {
        var dotProduct = Vector2.Dot(a, b);
        
        if (normalize) dotProduct /= a.Length() * b.Length();
        
        return MathF.Acos(dotProduct);
    }
}