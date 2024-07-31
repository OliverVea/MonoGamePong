using System.Collections.Generic;
using System.Linq;

using Shared.Geometry.Shapes;

namespace Entombed.Code.Game.Levels;

public class LevelCollisionService(Level level)
{
    public bool CollidesWithLevel(Circle playerCollider)
    {
        List<LineSegment> lineSegments = [];
        
        lineSegments.AddRange(level.Rooms.Values.SelectMany(x => x.Walls));
        lineSegments.AddRange(level.Doors.Values.Where(x => !x.Open).Select(x => x.LineSegment));

        return lineSegments.Any(x => playerCollider.Intersects(x));
    }
}