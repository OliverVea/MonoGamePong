using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Content;

namespace Pong.Core;

public class BallTextureLoader(GraphicsDevice graphicsDevice, GameProperties gameProperties) : IContentLoader<Texture2D>
{
    public IEnumerable<(Id<Texture2D> ContentId, Texture2D Content)> Load()
    {
        var width = (int)(gameProperties.BallWidth * gameProperties.ScreenWidth);
        var height = (int)(gameProperties.BallHeight * gameProperties.ScreenHeight);
        
        var texture = new Texture2D(graphicsDevice, width, height);

        var color = new Color[width * height];
        for (var i = 0; i < width * height; i++) color[i] = Color.White;
        
        texture.SetData(color);

        return [(Ids.Ball, texture)];
    }
}