using Microsoft.Xna.Framework;

namespace MonogameTest;

public class GameInput
{
    public required PlayerInput LeftPlayerInput { get; init; }
    public required PlayerInput RightPlayerInput { get; init; }
    public required GameTime GameTime { get; init; }
}

public class PlayerInput
{
    public InputDirection Direction { get; init; }
}

public enum InputDirection
{
    None, Up, Down
}