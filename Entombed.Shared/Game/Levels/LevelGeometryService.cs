using System.Linq;
using Shared.Geometry.Shapes;

namespace Entombed.Game.Levels;

public class LevelGeometryService(Level level)
{
    public bool CollidesWithLevel(LineSegment lineSegment)
    {
        var geometry = level.Doors.Select(x => x.Line).Concat(level.Rooms.SelectMany(x => x.Walls)).ToArray();
        return geometry.Any(x => x.Intersects(lineSegment));
    }
}