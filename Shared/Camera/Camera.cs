using Microsoft.Xna.Framework;

namespace Shared.Camera;

public class Camera(CameraConfiguration cameraConfiguration, Screen.Screen screen)
{
    public Vector2 Position { get; set; } = cameraConfiguration.InitialPosition;

    /// <summary>
    /// Zoom level of the camera.
    /// 1 means 1m in the world is 1 pixel on the screen.
    /// 2 means 1m in the world is 2 pixels on the screen.
    /// </summary>
    public float Zoom { get; set; } = cameraConfiguration.InitialZoom;
    
    public Vector2 PositionToCamera(Vector2 worldPosition)
    {
        var screenPosition = new Vector2
        {
            X = (worldPosition.X - Position.X) * Zoom + screen.Center.X,
            Y = (worldPosition.Y - Position.Y) * Zoom + screen.Center.Y
        };
        
        return screenPosition;
    }
    
    public float DistanceToCamera(float worldDistance)
    {
        return worldDistance * Zoom;
    }
    
    public Vector2 PositionToWorld(Vector2 cameraPosition)
    {
        var worldPosition = new Vector2
        {
            X = (cameraPosition.X - screen.Center.X) / Zoom + Position.X,
            Y = (cameraPosition.Y - screen.Center.Y) / Zoom + Position.Y
        };
        
        return worldPosition;
    }
    
    public float DistanceToWorld(float cameraDistance)
    {
        return cameraDistance / Zoom;
    }
}