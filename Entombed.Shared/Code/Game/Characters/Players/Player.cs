using Microsoft.Xna.Framework;

namespace Entombed.Code.Game.Characters.Players;

public class Player : Character
{
    public float InteractionDistance => 0.5f;
    public override Color Color => Color.Green;
}