using Microsoft.Xna.Framework;
using Shared.Extensions;
using Shared.Lifetime;

namespace Shared.Metrics;

public class TimeMetricInputService(TimeMetrics timeMetrics, GameTime gameTime) : IUpdateService, IDrawService, IGuiService
{
    private DateTimeOffset _lastFrameTime = DateTimeOffset.Now;
    private DateTimeOffset _lastUpdateTime = DateTimeOffset.Now;
    private DateTimeOffset _lastGuiTime = DateTimeOffset.Now;


    public void Update()
    {
        var now = DateTimeOffset.Now;
        var deltaTime = (float)(now - _lastUpdateTime).TotalSeconds;
        _lastUpdateTime = now;

        timeMetrics.DeltaTime = gameTime.DeltaTime();
        timeMetrics.TickRate = 1 / deltaTime;
    }

    public void Draw()
    {
        var now = DateTimeOffset.Now;
        var deltaTime = (float)(now - _lastFrameTime).TotalSeconds;
        _lastFrameTime = now;

        timeMetrics.FrameRate = 1 / deltaTime;
    }

    public void DrawGui()
    {
        var now = DateTimeOffset.Now;
        var deltaTime = (float)(now - _lastGuiTime).TotalSeconds;
        _lastGuiTime = now;

        timeMetrics.GuiFrameRate = 1 / deltaTime;
    }
}