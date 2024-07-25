using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shared.Content;

namespace Pong.Core;

public class ContentLoader(ContentManager contentManager, GraphicsDevice graphicsDevice, GameProperties gameProperties) : 
    IContentLoader<SpriteFont>,
    IContentLoader<Texture2D>,
    IContentLoader<SoundEffect>,
    IContentLoader<Effect>
{

    public IEnumerable<(Id<SpriteFont> ContentId, SpriteFont Content)> Load()
    {
        var font = contentManager.Load<SpriteFont>("arial");
        
        yield return (Ids.Arial, font);
    }

    IEnumerable<(Id<Texture2D> ContentId, Texture2D Content)> IContentLoader<Texture2D>.Load()
    {
        yield return (Ids.Paddle, LoadPaddleTexture());
        yield return (Ids.Ball, LoadBallTexture());
    }

    private Texture2D LoadPaddleTexture()
    {
        var width = (int)(gameProperties.PaddleWidth * gameProperties.ScreenWidth);
        var height = (int)(gameProperties.PaddleHeight * gameProperties.ScreenHeight);
        
        var texture = new Texture2D(graphicsDevice, width, height);

        var color = new Color[width * height];
        for (var i = 0; i < width * height; i++) color[i] = Color.White;
        
        texture.SetData(color);

        return texture;
    }

    private Texture2D LoadBallTexture()
    {
        var width = (int)(gameProperties.BallWidth * gameProperties.ScreenWidth);
        var height = (int)(gameProperties.BallHeight * gameProperties.ScreenHeight);
        
        var texture = new Texture2D(graphicsDevice, width, height);

        var color = new Color[width * height];
        for (var i = 0; i < width * height; i++) color[i] = Color.White;
        
        texture.SetData(color);

        return texture;
    }

    IEnumerable<(Id<SoundEffect> ContentId, SoundEffect Content)> IContentLoader<SoundEffect>.Load()
    {
        yield return (Ids.Pong, contentManager.Load<SoundEffect>("pong"));
    }

    IEnumerable<(Id<Effect> ContentId, Effect Content)> IContentLoader<Effect>.Load()
    {
        var effect = contentManager.Load<Effect>("fonteffect");
        yield return (Ids.FontEffect, effect);
    }
}