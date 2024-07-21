using Microsoft.Xna.Framework;

namespace Shared.Screen;

public class Screen : RequireInitialization<ScreenData>
{
    public int Width => Value.Width;
    public int Height => Value.Height;
    
    public float AspectRatio => (float)Width / Height;
    public Vector2 Center => new(Width / 2f, Height / 2f);
}

public class ScreenData
{
    public int Width { get; set; }
    public int Height { get; set; }
}