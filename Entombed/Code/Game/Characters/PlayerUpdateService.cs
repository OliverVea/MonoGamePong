using System.Linq;
using Entombed.Code.Game.Levels;
using Shared.Geometry;
using Shared.Lifetime;
using Shared.Metrics;

namespace Entombed.Code.Game.Characters;

public class PlayerUpdateService(Player player, Level level, PlayerInput playerInput, TimeMetrics timeMetrics) : IUpdateService
{
    public void Update()
    {
        var newPosition = player.Position + playerInput.Movement * player.Speed * timeMetrics.DeltaTime;
        var characterCircle = new Circle(newPosition, player.Radius);
        
        var wallLineSegments = level.Rooms.SelectMany(room => room.Walls).ToArray();
        
        var intersects = wallLineSegments.Any(wallLineSegment => GeometryHelper.Overlaps(characterCircle, wallLineSegment));
        if (intersects) return;
        
        player.Position = newPosition;
    }
}