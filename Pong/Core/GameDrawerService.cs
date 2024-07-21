using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Content;
using Shared.Lifetime;

namespace Pong.Core;

public class GameDrawerService(
    SpriteBatch spriteBatch,
    GameState gameState,
    GameProperties gameProperties,
    ContentLookup<Texture2D> textureLookup) : IDrawService
{
    public void Draw()
    {
        spriteBatch.Begin();
        
        DrawPaddles();
        DrawBall();
        
        spriteBatch.End();
    }

    private void DrawPaddles()
    {
        DrawPaddle(gameProperties.PaddleIndent, gameState.LeftPaddleY);
        DrawPaddle(1 - gameProperties.PaddleIndent, gameState.RightPaddleY);
    }

    private void DrawPaddle(float paddleX, float paddleY)
    {
        var screenSpaceX = ToScreenSpace(paddleX, gameProperties.PaddleWidth, gameProperties.ScreenWidth);
        var screenSpaceY = ToScreenSpace(paddleY, gameProperties.PaddleHeight, gameProperties.ScreenHeight);
        
        var position = new Vector2(screenSpaceX, screenSpaceY);

        var paddleTexture = textureLookup.Get(Ids.Paddle);
        
        spriteBatch.Draw(paddleTexture, position, Color.White);
    }

    private void DrawBall()
    {
        var screenSpaceX = ToScreenSpace(gameState.BallPosition.X, gameProperties.BallWidth, gameProperties.ScreenWidth);
        var screenSpaceY = ToScreenSpace(gameState.BallPosition.Y, gameProperties.BallHeight, gameProperties.ScreenHeight);
        
        var position = new Vector2(screenSpaceX, screenSpaceY);

        var ballTexture = textureLookup.Get(Ids.Ball);
        
        spriteBatch.Draw(ballTexture, position, Color.White);
    }
    
    private static float ToScreenSpace(float gameSpaceValue, float gameSpaceSize, int screenSize)
    {
        var gameSpaceStart = gameSpaceValue - gameSpaceSize / 2;
        return gameSpaceStart * screenSize;
    }
}