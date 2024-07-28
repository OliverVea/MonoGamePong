using Microsoft.Xna.Framework;

namespace Shared.Screen;

public class Screen
{
    private readonly RequireInitialization<ScreenData> _screenData = new();
    public ScreenData Value
    {
        get => _screenData.Value;
        set => _screenData.Value = value;
    }
    
    public int Width => Value.Width;
    public int Height => Value.Height;
    
    public Vector2 Center => new(Width / 2f, Height / 2f);
}

public class ScreenData
{
    public int Width { get; set; }
    public int Height { get; set; }
}