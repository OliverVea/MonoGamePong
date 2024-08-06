using System.Collections.Generic;
using System.Linq;
using Entombed.Game.Characters.Players;
using Entombed.Game.Menu;
using Microsoft.Extensions.Logging;
using Shared.Lifetime;

namespace Entombed.Game.Characters.Enemies;

public class EnemyUpdateService(
    GamePaused gamePaused,
    PlayerInput playerInput,
    CharacterLookup characterLookup,
    ILogger<EnemyUpdateService> logger,
    IEnumerable<EnemyGoalBehavior> goalBehaviors) : IUpdateService
{
    public bool Active => !gamePaused.Paused;
    
    private readonly Dictionary<EnemyGoal, EnemyGoalBehavior> _enemyGoalBehaviorLookup = goalBehaviors.ToDictionary(x => x.Goal);
    
    public void Update()
    {
        if (playerInput.Pause) return;
        
        goalBehaviors = goalBehaviors.ToList();
        var enemies = characterLookup.Values.OfType<Enemy>();
        foreach (var enemy in enemies) UpdateEnemy(enemy);
    }

    private void UpdateEnemy(Enemy enemy)
    {
        foreach (var goal in enemy.Strategy)
        {
            if (!_enemyGoalBehaviorLookup.TryGetValue(goal, out var behavior))
            {
                logger.LogWarning("No behavior found for goal {Goal}", goal);
                continue;
            }

            var goalResult = behavior.Update(enemy);

            if (goalResult.IsT0)
            {
                enemy.LastGoal = goal;
                return;
            }
        }
        
        enemy.LastGoal = EnemyGoal.None;
    }
}