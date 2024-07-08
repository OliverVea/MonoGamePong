using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Models;

namespace Pong.Services;

public class GameDrawerService(GameState gameState, GameProperties gameProperties, GraphicsDevice graphicsDevice)
{
    private readonly RequireInitialization<Texture2D> _paddleTexture = new();
    private readonly RequireInitialization<Texture2D> _ballTexture = new();
    
    
    public void Initialize()
    {
        var screenSpacePaddleWidth = (int)ToScreenSpace(gameProperties.PaddleWidth, 0, gameProperties.ScreenWidth);
        var screenSpacePaddleHeight = (int)ToScreenSpace(gameProperties.PaddleHeight, 0, gameProperties.ScreenHeight);
        
        _paddleTexture.Value = new Texture2D(graphicsDevice, screenSpacePaddleWidth, screenSpacePaddleHeight);
        _ballTexture.Value = new Texture2D(graphicsDevice, screenSpacePaddleWidth, screenSpacePaddleWidth);
        
        var paddleData = CreateTextureData(screenSpacePaddleWidth, screenSpacePaddleHeight, Color.White);
        var ballData = CreateTextureData(screenSpacePaddleWidth, screenSpacePaddleWidth, Color.White);
        
        _paddleTexture.Value.SetData(paddleData);
        _ballTexture.Value.SetData(ballData);
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        DrawPaddles(spriteBatch);
        DrawBall(spriteBatch);
    }

    private void DrawPaddles(SpriteBatch spriteBatch)
    {
        DrawPaddle(spriteBatch, gameProperties.PaddleIndent, gameState.LeftPaddleY);
        DrawPaddle(spriteBatch, 1 - gameProperties.PaddleIndent, gameState.RightPaddleY);
    }

    private void DrawPaddle(SpriteBatch spriteBatch, float paddleX, float paddleY)
    {
        var screenSpaceX = ToScreenSpace(paddleX, gameProperties.PaddleWidth, gameProperties.ScreenWidth);
        var screenSpaceY = ToScreenSpace(paddleY, gameProperties.PaddleHeight, gameProperties.ScreenHeight);
        
        var position = new Vector2(screenSpaceX, screenSpaceY);
        
        spriteBatch.Draw(_paddleTexture.Value, position, Color.White);
    }

    private void DrawBall(SpriteBatch spriteBatch)
    {
        var screenSpaceX = ToScreenSpace(gameState.BallPosition.X, gameProperties.BallSize, gameProperties.ScreenWidth);
        var screenSpaceY = ToScreenSpace(gameState.BallPosition.Y, gameProperties.BallSize, gameProperties.ScreenHeight);
        
        var position = new Vector2(screenSpaceX, screenSpaceY);
        
        spriteBatch.Draw(_ballTexture.Value, position, Color.White);
    }
    
    private static float ToScreenSpace(float gameSpaceValue, float gameSpaceSize, int screenSize)
    {
        var gameSpaceStart = gameSpaceValue - gameSpaceSize / 2;
        return gameSpaceStart * screenSize;
    }
    
    private static Color[] CreateTextureData(int width, int height, Color color)
    {
        var data = new Color[width * height];
        
        for (var i = 0; i < data.Length; ++i)
        {
            data[i] = color;
        }

        return data;
    }
}