using System.Collections.Generic;
using System.Linq;

using Shared.Geometry.Shapes;

namespace Entombed.Code.Game.Levels;

public class LevelCollisionService(RoomLookup roomLookup, DoorLookup doorLookup)
{
    public bool CollidesWithLevel(Circle playerCollider)
    {
        List<LineSegment> lineSegments = [];
        
        lineSegments.AddRange(roomLookup.Values.SelectMany(x => x.Walls));
        lineSegments.AddRange(doorLookup.Values.Where(x => !x.Open).Select(x => x.LineSegment));

        return lineSegments.Any(x => playerCollider.Intersects(x));
    }
}