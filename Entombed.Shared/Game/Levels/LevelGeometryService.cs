using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;

namespace Entombed.Game.Levels;

public class LevelGeometryService
{
    public bool CollidesWithLevel(
        LineSegment lineSegment,
        Level level,
        CollisionType collisionType = CollisionType.Both)
    {
        var geometry = new List<LineSegment>();
        
        if (collisionType.HasFlag(CollisionType.Wall)) geometry.AddRange(level.Rooms.SelectMany(x => x.Walls));
        if (collisionType.HasFlag(CollisionType.Door)) geometry.AddRange(level.Doors.Select(x => x.Line));
        
        return geometry.Any(x => x.Intersects(lineSegment));
    }

    public bool CollidesWithLevel(
        Vector2 from,
        Vector2 to,
        Level level,
        CollisionType collisionType = CollisionType.Both)
    {
        var lineSegment = new LineSegment(from, to);
        return CollidesWithLevel(lineSegment, level, collisionType);
    }
}

[Flags]
public enum CollisionType
{
    None = 0,
    Wall = 1,
    Door = 2,
    Both = Wall | Door
}