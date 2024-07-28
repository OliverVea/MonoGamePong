using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;
using Rectangle = Shared.Geometry.Shapes.Rectangle;

namespace Shared.Geometry.Helpers;

internal partial class ContainsHelper
{
    public static bool Contains(Rectangle rectangle, Vector2 point)
    {
        return point.X >= rectangle.TopLeft.X && point.X <= rectangle.TopRight.X &&
               point.Y >= rectangle.TopLeft.Y && point.Y <= rectangle.BottomLeft.Y;
    }

    public static bool Contains(Rectangle rectangle, LineSegment lineSegment)
    {
        return Contains(rectangle, lineSegment.Start) && Contains(rectangle, lineSegment.End);
    }

    public static bool Contains(Rectangle rectangle, Rectangle otherRectangle)
    {
        return Contains(rectangle, otherRectangle.TopLeft) && Contains(rectangle, otherRectangle.TopRight) &&
               Contains(rectangle, otherRectangle.BottomLeft) && Contains(rectangle, otherRectangle.BottomRight);
    }
}