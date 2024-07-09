using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Core.Models;
using Pong.Core.Textures;

namespace Pong.Core.Services;

public class GameDrawerService(
    GameState gameState,
    GameProperties gameProperties,
    TextureLookup textureLookup)
{
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

        var paddleTexture = textureLookup.GetTexture(TextureIds.Paddle);
        
        spriteBatch.Draw(paddleTexture, position, Color.White);
    }

    private void DrawBall(SpriteBatch spriteBatch)
    {
        var screenSpaceX = ToScreenSpace(gameState.BallPosition.X, gameProperties.BallWidth, gameProperties.ScreenWidth);
        var screenSpaceY = ToScreenSpace(gameState.BallPosition.Y, gameProperties.BallHeight, gameProperties.ScreenHeight);
        
        var position = new Vector2(screenSpaceX, screenSpaceY);

        var ballTexture = textureLookup.GetTexture(TextureIds.Ball);
        
        spriteBatch.Draw(ballTexture, position, Color.White);
    }
    
    private static float ToScreenSpace(float gameSpaceValue, float gameSpaceSize, int screenSize)
    {
        var gameSpaceStart = gameSpaceValue - gameSpaceSize / 2;
        return gameSpaceStart * screenSize;
    }
}