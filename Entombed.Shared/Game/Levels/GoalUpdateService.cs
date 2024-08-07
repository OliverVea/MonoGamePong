using Entombed.Game.Characters.Players;
using Shared.Lifetime;

namespace Entombed.Game.Levels;

public class GoalUpdateService(Level level, Player player) : IUpdateService
{

    public void Update()
    {
        if (!player.CarryingGoal) return;
        
        level.Goal = player.Position;
    }
}