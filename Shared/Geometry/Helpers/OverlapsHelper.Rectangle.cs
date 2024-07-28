using Shared.Geometry.Shapes;

namespace Shared.Geometry.Helpers;

internal static partial class OverlapsHelper
{
    public static bool Overlaps(Rectangle rectangle, Rectangle otherRectangle)
    {
        return rectangle.TopLeft.X < otherRectangle.BottomRight.X &&
               rectangle.BottomRight.X > otherRectangle.TopLeft.X &&
               rectangle.TopLeft.Y < otherRectangle.BottomRight.Y &&
               rectangle.BottomRight.Y > otherRectangle.TopLeft.Y;
    }
}