using System;
using System.Linq;
using Entombed.Game.Characters.Enemies;
using Entombed.Game.Levels;
using Entombed.Game.Menu;
using Entombed.Loading;
using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Shared.Lifetime;
using Shared.Metrics;
using Shared.Scenes;

namespace Entombed.Game.Characters.Players;

public class PlayerUpdateService(
    Level level,
    GamePaused gamePaused,
    Player player,
    PlayerInput playerInput,
    CharacterLookup characterLookup,
    CharacterDamageService characterDamageService,
    TimeMetrics timeMetrics,
    LevelCollisionService levelCollisionService,
    DoorService doorService,
    RoomLookup roomLookup,
    SceneManager sceneManager,
    DoorLookup doorLookup) : IUpdateService
{
    public bool Active => !gamePaused.Paused;
    
    public void Update()
    {
        if (playerInput.Pause) return;
        
        UpdatePlayerMovement();
        UpdatePlayerInteraction();
        UpdatePlayerAttack();
        UpdatePlayerLight();
    }

    private void UpdatePlayerMovement()
    {
        var newPosition = player.Position + playerInput.Movement * player.Speed * timeMetrics.DeltaTime * (player.CarryingGoal ? 0.4f : 1);
        var characterCircle = new Circle(newPosition, player.Radius);
        
        if (playerInput.Movement != Vector2.Zero)  player.Direction = Vector2.Normalize(playerInput.Movement);

        var movementCollidesWithLevel = levelCollisionService.CollidesWithLevel(characterCircle);
        if (movementCollidesWithLevel) return;
        
        player.Position = newPosition;
    }

    private void UpdatePlayerInteraction()
    {
        if (!playerInput.Use) return;

        if (TryUseDoor()) return;
        if (TryUseGoal()) return;
        if (TryUseStairs()) return;
    }

    private bool TryUseDoor()
    {
        var closestDoor = doorLookup.Values.Where(x => !x.Open)
            .Select(x => (Door: x, Distance: x.Line.Distance(player.Position)))
            .OrderBy(x => x.Distance)
            .FirstOrDefault();
        
        if (closestDoor == default) return false;
        
        if (closestDoor.Distance > player.InteractionDistance) return false;
        
        doorService.OpenDoor(closestDoor.Door.Id);
        
        return true;
    }

    private bool TryUseGoal()
    {
        if (player.CarryingGoal) return false;
        
        var distanceToGoal = Vector2.Distance(player.Position, level.Goal);
        
        if (distanceToGoal > player.InteractionDistance) return false;
        
        doorService.OpenAllDoors();
        player.CarryingGoal = true;
        
        return true;
    }

    private bool TryUseStairs()
    {
        if (!player.CarryingGoal) return false;
        
        var distanceToStairs = Vector2.Distance(player.Position, level.Stairs);
        
        if (distanceToStairs > player.InteractionDistance) return false;
        
        sceneManager.Transition<LoadingScene>();
        
        return true;
    }

    private void UpdatePlayerAttack()
    {
        if (!playerInput.Attack) return;

        var enemies = characterLookup.Values.OfType<Enemy>().ToArray();
        var enemiesInRange = enemies.Where(x => DistanceToPlayer(x) < player.AttackRange).ToArray();
        var hitEnemies = enemiesInRange.Where(x => AngleToPlayer(x) < player.AttackAngle).ToArray();
        
        foreach (var enemy in hitEnemies) characterDamageService.Damage(player.Id, enemy.Id, player.AttackDamage);
    }

    private void UpdatePlayerLight()
    {
        if (!playerInput.Light) return;
        
        var roomResult = roomLookup.GetRoomWithPosition(player.Position);
        if (roomResult.IsT1) return;
        
        var roomId = roomResult.AsT0;
        var room = roomLookup.Get(roomId);
        
        room.Lit = !room.Lit;
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