using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameTest;

public class GameDrawerService(GameState gameState, GameProperties gameProperties, GraphicsDevice graphicsDevice)
{
    private Texture2D? _paddleTexture;
    private Texture2D? _ballTexture;
    
    private Texture2D PaddleTexture => _paddleTexture ?? throw new System.Exception("Paddle texture not initialized");
    private Texture2D BallTexture => _ballTexture ?? throw new System.Exception("Ball texture not initialized");
    
    public void Initialize()
    {
        var screenSpacePaddleWidth = ToScreenSpace(gameProperties.PaddleWidth, 0, gameProperties.ScreenWidth);
        
        
        
        _paddleTexture = new Texture2D(graphicsDevice, 20, 200);
        _ballTexture = new Texture2D(graphicsDevice, 20, 20);
        
        var paddleData = CreateTextureData(20, 200, Color.White);
        var ballData = CreateTextureData(20, 20, Color.White);
        
        PaddleTexture.SetData(paddleData);
        BallTexture.SetData(ballData);
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        DrawPaddles(spriteBatch);
        DrawBall(spriteBatch);
    }

    private void DrawPaddles(SpriteBatch spriteBatch)
    {
        DrawPaddle(spriteBatch, 0.05f, gameState.LeftPaddleY);
        DrawPaddle(spriteBatch, 0.95f, gameState.RightPaddleY);
    }

    private void DrawPaddle(SpriteBatch spriteBatch, float paddleX, float paddleY)
    {
        var screenSpaceX = ToScreenSpace(paddleX, gameProperties.PaddleWidth, gameProperties.ScreenWidth);
        var screenSpaceY = ToScreenSpace(paddleY, gameProperties.PaddleHeight, gameProperties.ScreenHeight);
        
        var position = new Vector2(screenSpaceX, screenSpaceY);
        
        spriteBatch.Draw(PaddleTexture, position, Color.Green);
    }

    private void DrawBall(SpriteBatch spriteBatch)
    {
        var screenSpaceX = ToScreenSpace(gameState.BallPosition.X, gameProperties.BallSize, gameProperties.ScreenWidth);
        var screenSpaceY = ToScreenSpace(gameState.BallPosition.Y, gameProperties.BallSize, gameProperties.ScreenHeight);
        
        var position = new Vector2(screenSpaceX, screenSpaceY);
        
        spriteBatch.Draw(BallTexture, position, Color.White);
    }
    
    
    
    // Utility
    
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