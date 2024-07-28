using Microsoft.Xna.Framework;
using Shared.Geometry.Shapes;

namespace Shared.Camera;

public class IsometricCamera(IsometricCameraConfiguration isometricCameraConfiguration, Screen.Screen screen)
{
    public Vector2 Position { get; set; } = isometricCameraConfiguration.InitialPosition;

    /// <summary>
    /// Zoom level of the camera.
    /// 1 means 1m in the world is 1 pixel on the screen.
    /// 2 means 1m in the world is 2 pixels on the screen.
    /// </summary>
    public float Zoom { get; set; } = isometricCameraConfiguration.InitialZoom;
    
    public Vector2 WorldToScreen(Vector2 worldPosition)
    {
        var screenPosition = new Vector2
        {
            X = (worldPosition.X - Position.X) * Zoom + screen.Center.X,
            Y = -(worldPosition.Y - Position.Y) * Zoom + screen.Center.Y
        };
        
        return screenPosition;
    }
    
    public float WorldToScreen(float worldDistance)
    {
        return worldDistance * Zoom;
    }
    
    public LineSegment WorldToScreen(LineSegment screenLineSegment)
    {
        var start = WorldToScreen(screenLineSegment.Start);
        var end = WorldToScreen(screenLineSegment.End);
        
        return new LineSegment(start, end);
    }
    
    public Circle WorldToScreen(Circle screenCircle)
    {
        var center = WorldToScreen(screenCircle.Center);
        var radius = WorldToScreen(screenCircle.Radius);
        
        return new Circle(center, radius);
    }
    
    public Vector2 ScreenToWorld(Vector2 cameraPosition)
    {
        var worldPosition = new Vector2
        {
            X = (cameraPosition.X - screen.Center.X) / Zoom + Position.X,
            Y = -(cameraPosition.Y - screen.Center.Y) / Zoom + Position.Y
        };
        
        return worldPosition;
    }
    
    public float ScreenToWorld(float cameraDistance)
    {
        return cameraDistance / Zoom;
    }
}