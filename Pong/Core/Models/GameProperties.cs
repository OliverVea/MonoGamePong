namespace Pong.Core.Models;

public class GameProperties
{
    public int ScreenWidth { get; set; } = 800;
    public int ScreenHeight { get; set; } = 450;
    public float ScreenAspectRatio => (float)ScreenWidth / ScreenHeight;
    public float PaddleSpeed { get; set; } = 1.5f;
    public float BallSpeed { get; set; } = 0.5f;
    public float PaddleWidth { get; set; } = 0.02f;
    public float PaddleHeight { get; set; } = 0.2f;
    public float BallWidth { get; set; } = 0.02f;
    public float BallHeight => BallWidth * ScreenAspectRatio;
    public float PaddleIndent { get; set; } = 0.075f;
}