using System.Linq;
using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;

namespace Entombed.Game.Levels;

public class LevelGeometryService
{
    public bool CollidesWithLevel(LineSegment lineSegment, Level level)
    {
        var geometry = level.Doors.Select(x => x.Line).Concat(level.Rooms.SelectMany(x => x.Walls)).ToArray();
        return geometry.Any(x => x.Intersects(lineSegment));
    }

    public bool CollidesWithLevel(Vector2 from, Vector2 to, Level level) => CollidesWithLevel(new LineSegment(from, to), level);
}