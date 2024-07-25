namespace Pong.Core;

public class GameInput
{
    public PlayerInput LeftPlayerInput { get; } = new();
    public PlayerInput RightPlayerInput { get; } = new();
    public bool UseTextShader { get; set; } = true;
}