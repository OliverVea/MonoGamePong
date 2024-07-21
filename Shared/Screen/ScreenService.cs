using Microsoft.Xna.Framework.Graphics;
using Shared.Lifetime;

namespace Shared.Screen;

public class ScreenService(GraphicsDevice graphicsDevice, Screen screen) : IInputService, IStartupService
{
    public int StartupPriority => 100;
    
    public void Input()
    {
        screen.Value.Width = graphicsDevice.Viewport.Width;
        screen.Value.Height = graphicsDevice.Viewport.Height;
    }

    public void Startup()
    {
        screen.Value = new ScreenData
        {
            Width = graphicsDevice.Viewport.Width,
            Height = graphicsDevice.Viewport.Height
        };
    }
}