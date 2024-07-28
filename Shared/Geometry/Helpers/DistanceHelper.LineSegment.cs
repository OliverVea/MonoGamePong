using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;

namespace Shared.Geometry.Helpers;

internal static partial class DistanceHelper
{
    public static float Distance(LineSegment lineSegment, Vector2 point)
    {
        var closestPoint = ClosestPoint(lineSegment, point);

        return Vector2.Distance(closestPoint, point);
        
        Vector2 ClosestPoint(LineSegment lineSegment1, Vector2 vector2)
        {
            var vector21 = vector2 - lineSegment1.Start;
            var vector22 = lineSegment1.End - lineSegment1.Start;
            var num = Vector2.Dot(vector22, vector21) / vector22.LengthSquared();
            if (num < 0f)
            {
                return lineSegment1.Start;
            }
            if (num > 1f)
            {
                return lineSegment1.End;
            }
            return lineSegment1.Start + vector22 * num;
        }
    }
    
}