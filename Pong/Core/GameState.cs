using Microsoft.Xna.Framework;

namespace Pong.Core;

public class GameState
{
    public float LeftPaddleY { get; set; } = 0.5f;
    public float RightPaddleY { get; set; } = 0.5f;
    
    public Vector2 BallPosition { get; set; } = new(0.5f, 0.5f);
    public Vector2 BallVelocity { get; set; } = new(1, 1);
}