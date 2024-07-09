using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Core.Models;

namespace Pong.Core.Textures;

public class PaddleTextureLoader(GameProperties gameProperties) : ITextureLoader
{
    public (Id<Texture2D> TextureId, Texture2D Texture) LoadTexture(GraphicsDevice graphicsDevice)
    {
        var width = (int)(gameProperties.PaddleWidth * gameProperties.ScreenWidth);
        var height = (int)(gameProperties.PaddleHeight * gameProperties.ScreenHeight);
        
        var texture = new Texture2D(graphicsDevice, width, height);

        var color = new Color[width * height];
        for (var i = 0; i < width * height; i++) color[i] = Color.White;
        
        texture.SetData(color);

        return (TextureIds.Paddle, texture);
    }
}