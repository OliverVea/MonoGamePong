using System;
using Microsoft.Xna.Framework;

namespace MonogameTest;

public class GameLogicService(GameState gameState, GameProperties gameProperties)
{
    public void Update(GameInput gameInput)
    {
        UpdatePaddles(gameInput);
        //UpdateBall();
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
    
    private float GetFrameSpeed(GameTime gametime, float speed) => gametime.DeltaTime() * speed;
}