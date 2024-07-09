using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Core.Models;

namespace Pong.Core.Textures;

public class BallTextureLoader(GameProperties gameProperties) : ITextureLoader
{
    public (Id<Texture2D> TextureId, Texture2D Texture) LoadTexture(GraphicsDevice graphicsDevice)
    {
        var width = (int)(gameProperties.BallWidth * gameProperties.ScreenWidth);
        var height = (int)(gameProperties.BallHeight * gameProperties.ScreenHeight);
        
        var texture = new Texture2D(graphicsDevice, width, height);

        var color = new Color[width * height];
        for (var i = 0; i < width * height; i++) color[i] = Color.White;
        
        texture.SetData(color);

        return (TextureIds.Ball, texture);
    }
}