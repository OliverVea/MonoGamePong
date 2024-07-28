using System.Linq;
using Entombed.Code.Game.Levels;
using Shared.Geometry.Shapes;
using Shared.Lifetime;
using Shared.Metrics;

namespace Entombed.Code.Game.Characters.Players;

public class PlayerUpdateService(
    Player player,
    PlayerInput playerInput,
    TimeMetrics timeMetrics,
    LevelCollisionService levelCollisionService,
    DoorService doorService,
    Level level) : IUpdateService
{
    public void Update()
    {
        UpdatePlayerMovement();
        UpdatePlayerInteraction();
    }

    private void UpdatePlayerMovement()
    {
        var newPosition = player.Position + playerInput.Movement * player.Speed * timeMetrics.DeltaTime;
        var characterCircle = new Circle(newPosition, player.Radius);

        var movementCollidesWithLevel = levelCollisionService.CollidesWithLevel(characterCircle);
        if (movementCollidesWithLevel) return;
        
        player.Position = newPosition;
    }

    private void UpdatePlayerInteraction()
    {
        if (!playerInput.Use) return;
        
        var (closestDoor, distance) = level.Doors
            .Select(x => (Door: x, Distance: x.LineSegment.Distance(player.Position)))
            .MinBy(x => x.Distance);
        
        if (distance > player.InteractionDistance) return;
        
        doorService.OpenDoor(closestDoor);
    }
}