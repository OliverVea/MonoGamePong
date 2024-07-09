using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shared;

public class TextureAtlas
{
    public required Texture2D Texture { get; init; }
    public required int SpriteHeight { get; init; }
    public required int SpriteWidth { get; init; }
    
    public int Rows => Texture.Height / SpriteHeight;
    public int Cols => Texture.Width / SpriteWidth;

    public Rectangle GetSourceRectangle(int frame)
    {
        var x = frame % Cols;
        var y = frame / Cols;

        return new Rectangle(x * SpriteWidth, y * SpriteHeight, SpriteWidth, SpriteHeight);
    }
}