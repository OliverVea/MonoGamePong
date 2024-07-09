using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shared;

public class SpriteAnimation
{
    public required TextureAtlas TextureAtlas { get; init; }
    public required int[] FrameIndices;
    public float FrameRate { get; set; } = 1.5f;

    public (Texture2D Texture, Rectangle SourceRectangle) GetTexture(float elapsedSeconds)
    {
        var frameIndexIndex = (int)(elapsedSeconds % (1f / FrameRate) * FrameRate * FrameIndices.Length);
        var frameIndex = FrameIndices[frameIndexIndex];
        var frame = TextureAtlas.GetSourceRectangle(frameIndex);

        return (TextureAtlas.Texture, frame);
    }
}