using System;
using Microsoft.Xna.Framework;
using Pong.Core.Models;
using Shared.Extensions;

namespace Pong.Core.Services;

public class GameLogicService(GameState gameState, GameProperties gameProperties)
{
    public void Update(GameInput gameInput)
    {
        UpdatePaddles(gameInput);
        UpdateBall(gameInput);
    }

    private void UpdatePaddles(GameInput gameInput)
    {
        gameState.LeftPaddleY = UpdatePaddle(gameState.LeftPaddleY, gameInput.LeftPlayerInput.Direction, gameInput.GameTime);
        gameState.RightPaddleY = UpdatePaddle(gameState.RightPaddleY, gameInput.RightPlayerInput.Direction, gameInput.GameTime);
    }

    private float UpdatePaddle(float paddleY, InputDirection direction, GameTime gameTime)
    {
        var paddleSpeed = GetFrameSpeed(gameTime, gameProperties.PaddleSpeed);
        var change = direction switch
        {
            InputDirection.Up => -paddleSpeed,
            InputDirection.Down => paddleSpeed,
            _ => 0
        };
        
        var minHeight = gameProperties.PaddleHeight / 2;
        var maxHeight = 1 - gameProperties.PaddleHeight / 2;
        
        return Math.Clamp(paddleY + change, minHeight, maxHeight);
    }

    private void UpdateBall(GameInput gameInput)
    {
        var nextPosition = CalculateNextPosition(gameInput);
        
        var collidesWithLeftPaddle = CollidesWithPaddle(nextPosition, gameProperties.PaddleIndent, gameState.LeftPaddleY);
        var collidesWithRightPaddle = CollidesWithPaddle(nextPosition, 1 - gameProperties.PaddleIndent, gameState.RightPaddleY);

        if (collidesWithLeftPaddle || collidesWithRightPaddle)
        {
            gameState.BallVelocity = gameState.BallVelocity.FlipHorizontal();
            nextPosition = CalculateNextPosition(gameInput);
        }

        var collidesWithTop = nextPosition.Y < gameProperties.BallHeight;
        var collidesWithBottom = nextPosition.Y >= 1 - gameProperties.BallHeight;

        if (collidesWithTop || collidesWithBottom)
        {
            gameState.BallVelocity = gameState.BallVelocity.FlipVertical();
            nextPosition = CalculateNextPosition(gameInput);
        }

        gameState.BallPosition = nextPosition;
    }

    private Vector2 CalculateNextPosition(GameInput gameInput)
    {
        var speed = GetFrameSpeed(gameInput.GameTime, gameProperties.BallSpeed);
        var delta = gameState.BallVelocity * speed;
        return gameState.BallPosition + delta;
    }

    private bool CollidesWithPaddle(Vector2 ballPosition, float paddleX, float paddleY)
    {
        var dx = Math.Abs(ballPosition.X - paddleX);
        var dy = Math.Abs(ballPosition.Y - paddleY);

        return dx <= (gameProperties.PaddleWidth + gameProperties.BallWidth) / 2 && 
               dy <= (gameProperties.PaddleHeight + gameProperties.BallHeight) / 2;
    }
    
    private float GetFrameSpeed(GameTime gametime, float speed) => gametime.DeltaTime() * speed;
}