using Microsoft.Xna.Framework;

namespace Entombed.Code.Game.Characters.Players;

public class PlayerInput
{
    public Vector2 Movement { get; set; } = Vector2.Zero;
    public bool Use { get; set; } = false;
}