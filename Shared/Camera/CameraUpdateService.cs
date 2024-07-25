using Microsoft.Xna.Framework;
using Shared.Extensions;
using Shared.Lifetime;

namespace Shared.Camera;

public class CameraUpdateService(CameraInput cameraInput, Camera camera, GameTime gameTime) : IUpdateService
{
    public void Update()
    {
        cameraInput.Zoom.Switch(_ => { }, SetZoom, ZoomVelocity);
        cameraInput.Position.Switch(_ => { }, SetPosition, MoveVelocity);
    }
    
    private void SetZoom(CameraInput.SetZoom setZoom) => camera.Zoom = setZoom.Value;
    private void ZoomVelocity(CameraInput.ZoomVelocity zoomVelocity) => camera.Zoom *= 1 + (zoomVelocity.Value - 1) * gameTime.DeltaTime();
    private void SetPosition(CameraInput.SetPosition setPosition) => camera.Position = setPosition.Value;
    private void MoveVelocity(CameraInput.MoveVelocity moveVelocity) => camera.Position += moveVelocity.Value * gameTime.DeltaTime();
}