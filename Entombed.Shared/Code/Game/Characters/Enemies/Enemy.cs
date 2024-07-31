using Microsoft.Xna.Framework;

namespace Entombed.Code.Game.Characters.Enemies;

public class Enemy : Character
{
    public override Color Color => Color.Red;
    public EnemyGoal[] Strategy { get; set; } = [
        EnemyGoal.FightPlayer
    ];
}