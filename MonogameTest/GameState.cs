using SharpDX;

namespace MonogameTest;

public class GameState
{
    public float LeftPaddleY { get; set; } = 0.5f;
    public float RightPaddleY { get; set; } = 0.5f;
    
    public Vector2 BallPosition { get; set; } = new(0.5f, 0.5f);
    public Vector2 BallVelocity { get; set; } = new(1, 1);
}

public class GameProperties
{
    public int ScreenWidth { get; set; } = 800;
    public int ScreenHeight { get; set; } = 450;
    public float ScreenAspectRatio => (float)ScreenWidth / ScreenHeight;
    public float PaddleSpeed { get; set; } = 1.5f;
    public float BallSpeed { get; set; } = 0.5f;
    public float PaddleWidth { get; set; } = 0.02f;
    public float PaddleHeight { get; set; } = 0.1f;
    public float BallSize { get; set; } = 0.02f;
}