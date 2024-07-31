/*
Retrieved from: https://github.com/Fullbrik/MonoGame.Primitives2D/tree/master
 
This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

   1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.

   2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.

   3. This notice may not be removed or altered from any source
   distribution.
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Geometry.Shapes;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Shared;

public static class Primitives2D
{
    private const float DefaultLineThickness = 1.0f;
    private const float DefaultAngle = 0;
    
    private static readonly Dictionary<string, IReadOnlyList<Vector2>> CircleCache = new();
    private static Texture2D? _pixel;
    
    private static void CreateThePixel(SpriteBatch spriteBatch)
    {
        _pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        _pixel.SetData([Color.White]);
    }
    
    private static void DrawPoints(SpriteBatch spriteBatch, Vector2 position, IReadOnlyList<Vector2> points, Color color, float thickness)
    {
        if (points.Count < 2)
            return;

        for (var i = 1; i < points.Count; i++)
        {
            DrawLine(spriteBatch, points[i - 1] + position, points[i] + position, color, thickness);
        }
    }
    
    private static IReadOnlyList<Vector2> CreateCircle(float radius, int sides)
    {
        var circleKey = radius + "x" + sides;

        if (CircleCache.TryGetValue(circleKey, value: out var circle))
        {
            return circle;
        }

        var vectors = new Vector2[sides + 1];

        var step = MathHelper.TwoPi / sides;

        for (var i = 0; i < sides; i++)
        {
            var theta = i * step;
            
            vectors[i] = new Vector2(radius * MathF.Cos(theta), radius * MathF.Sin(theta));
        }

        vectors[^1] = vectors[0];

        CircleCache.Add(circleKey, vectors);

        return vectors;
    }
    
    private static IReadOnlyList<Vector2> CreateArc(float radius, int sides, float startingAngle, float radians)
    {
        var points = new List<Vector2>();
        points.AddRange(CreateCircle(radius, sides));
        points.RemoveAt(points.Count - 1);

        var curAngle = 0.0;
        double anglePerSide = MathHelper.TwoPi / sides;

        while (curAngle + anglePerSide / 2.0 < startingAngle)
        {
            curAngle += anglePerSide;

            points.Add(points[0]);
            points.RemoveAt(0);
        }

        points.Add(points[0]);

        var sidesInArc = (int)((radians / anglePerSide) + 0.5);
        points.RemoveRange(sidesInArc + 1, points.Count - sidesInArc - 1);

        return points;
    }

    public static void FillRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color)
    {
        if (_pixel == null) CreateThePixel(spriteBatch);

        // Simply use the function already there
        spriteBatch.Draw(_pixel, rect, color);
    }

    public static void FillRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float angle)
    {
        if (_pixel == null) CreateThePixel(spriteBatch);

        spriteBatch.Draw(
            _pixel,
            rect, 
            null, 
            color, 
            angle,
            Vector2.Zero, 
            SpriteEffects.None, 
            0);
    }

    public static void FillRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float angle = DefaultAngle)
    {
        if (_pixel == null) CreateThePixel(spriteBatch);

        // stretch the pixel between the two vectors
        spriteBatch.Draw(_pixel,
            location,
            null,
            color,
            angle,
            Vector2.Zero,
            size,
            SpriteEffects.None,
            0);
    }

    public static void FillRectangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color, float angle = DefaultAngle)
        => FillRectangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, angle);

    public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float thickness = DefaultLineThickness)
    {
        // TODO: Handle rotations
        // TODO: Figure out the pattern for the offsets required and then handle it in the line instead of here

        DrawLine(spriteBatch, new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, thickness);
        DrawLine(spriteBatch, new Vector2(rect.X + 1f, rect.Y), new Vector2(rect.X + 1f, rect.Bottom + thickness), color, thickness);
        DrawLine(spriteBatch, new Vector2(rect.X, rect.Bottom), new Vector2(rect.Right, rect.Bottom), color, thickness);
        DrawLine(spriteBatch, new Vector2(rect.Right + 1f, rect.Y), new Vector2(rect.Right + 1f, rect.Bottom + thickness), color, thickness);
    }

    public static void DrawRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float thickness = DefaultLineThickness)
        => DrawRectangle(spriteBatch, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, thickness);

    public static void DrawLineSegment(this SpriteBatch spriteBatch, LineSegment lineSegment, Color color, float thickness = DefaultLineThickness)
        => DrawLine(spriteBatch, lineSegment.Start, lineSegment.End, color, thickness);

    public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color, float thickness = DefaultLineThickness)
        => DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, thickness);

    public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness = DefaultLineThickness)
    {
        var distance = Vector2.Distance(point1, point2);

        var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

        DrawLine(spriteBatch, point1, distance, angle, color, thickness);
    }

    public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness = DefaultLineThickness)
    {
        if (_pixel == null) CreateThePixel(spriteBatch);

        spriteBatch.Draw(_pixel,
            point,
            null,
            color,
            angle,
            Vector2.Zero,
            new Vector2(length, thickness),
            SpriteEffects.None,
            0);
    }

    public static void PutPixel(this SpriteBatch spriteBatch, float x, float y, Color color)
        => PutPixel(spriteBatch, new Vector2(x, y), color);
    
    public static void PutPixel(this SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        if (_pixel == null) CreateThePixel(spriteBatch);
        spriteBatch.Draw(_pixel, position, color);
    }

    public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, Color color, float thickness = DefaultLineThickness)
        => DrawCircle(spriteBatch, center.X, center.Y, radius, sides, color, thickness);

    public static void DrawCircle(this SpriteBatch spriteBatch, float x, float y, float radius, int sides, Color color, float thickness = DefaultLineThickness)
    {
        var circle = CreateCircle(radius, sides);
        DrawPoints(spriteBatch, new Vector2(x, y), circle, color, thickness);
    }

    public static void DrawArc(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, float startingAngle, float radians, Color color, float thickness = DefaultLineThickness)
    {
        var arc = CreateArc(radius, sides, startingAngle, radians);
        DrawPoints(spriteBatch, center, arc, color, thickness);
    }
}