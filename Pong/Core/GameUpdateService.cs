using System;
using Microsoft.Xna.Framework;
using Shared.Events;
using Shared.Extensions;
using Shared.Lifetime;

namespace Pong.Core;

public class GameUpdateService(
    IEventInvoker<BallHitObjectEvent> ballHitObjectEventInvoker,
    GameInput gameInput,
    GameTime gameTime,
    GameState gameState,
    GameProperties gameProperties) : IUpdateService
{
    public void Update()
    {
        UpdatePaddles();
        UpdateBall();
    }

    private void UpdatePaddles()
    {
        gameState.LeftPaddleY = UpdatePaddle(gameState.LeftPaddleY, gameInput.LeftPlayerInput.Direction);
        gameState.RightPaddleY = UpdatePaddle(gameState.RightPaddleY, gameInput.RightPlayerInput.Direction);
    }

    private float UpdatePaddle(float paddleY, InputDirection direction)
    {
        var paddleSpeed = GetFrameSpeed(gameProperties.PaddleSpeed);
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

    private void UpdateBall()
    {
        var nextPosition = CalculateNextPosition();
        
        var collidesWithLeftPaddle = CollidesWithPaddle(nextPosition, gameProperties.PaddleIndent, gameState.LeftPaddleY);
        var collidesWithRightPaddle = CollidesWithPaddle(nextPosition, 1 - gameProperties.PaddleIndent, gameState.RightPaddleY);

        if (collidesWithLeftPaddle || collidesWithRightPaddle)
        {
            gameState.BallVelocity = gameState.BallVelocity.FlipHorizontal();
            nextPosition = CalculateNextPosition();
            ballHitObjectEventInvoker.Invoke(new BallHitObjectEvent());
        }

        var collidesWithTop = nextPosition.Y < gameProperties.BallHeight;
        var collidesWithBottom = nextPosition.Y >= 1 - gameProperties.BallHeight;

        if (collidesWithTop || collidesWithBottom)
        {
            gameState.BallVelocity = gameState.BallVelocity.FlipVertical();
            nextPosition = CalculateNextPosition();
            ballHitObjectEventInvoker.Invoke(new BallHitObjectEvent());
        }

        gameState.BallPosition = nextPosition;
    }

    private Vector2 CalculateNextPosition()
    {
        var speed = GetFrameSpeed(gameProperties.BallSpeed);
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
    
    private float GetFrameSpeed(float speed) => gameTime.DeltaTime() * speed;
}