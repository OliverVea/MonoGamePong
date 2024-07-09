using Microsoft.Xna.Framework;

namespace Pong.Core.Models;

public class GameInput
{
    public required PlayerInput LeftPlayerInput { get; init; }
    public required PlayerInput RightPlayerInput { get; init; }
    public required GameTime GameTime { get; init; }
}