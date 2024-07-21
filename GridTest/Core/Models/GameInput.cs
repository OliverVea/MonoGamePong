using Microsoft.Xna.Framework;

namespace ShaderSandbox.Core.Models;

public class GameInput
{
    public required Vector2 MousePosition { get; set; }
    public bool ExitGame { get; set; }
}