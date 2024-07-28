using Entombed.Code.Game.Characters;
using Entombed.Code.Game.Characters.Players;
using Shared.Camera;
using Shared.Lifetime;
using Shared.Metrics;

namespace Entombed.Code.Game.Camera;

public class CameraUpdateService(
    Player player,
    CameraInput cameraInput,
    IsometricCamera isometricCamera,
    TimeMetrics timeMetrics) : IUpdateService
{
    private const float ZoomSpeed = 1f;
    public int UpdatePriority => -1;

    public void Update()
    {
        isometricCamera.Position = player.Position;
        
        var zoom = cameraInput.CameraZoomAction switch
        {
            CameraZoomAction.ZoomIn => ZoomSpeed * timeMetrics.DeltaTime,
            CameraZoomAction.ZoomOut => -ZoomSpeed * timeMetrics.DeltaTime,
            _ => 0
        };
        
        isometricCamera.Zoom *= 1 + zoom;
    }
}