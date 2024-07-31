using System.Linq;
using Shared.Lifetime;

namespace Entombed.Code.Game.Characters.Enemies;

public class EnemyUpdateService(CharacterLookup characterLookup) : IUpdateService
{
    public void Update()
    {
        foreach (var enemy in characterLookup.Values.OfType<Enemy>())
        {
            UpdateEnemy(enemy);
        }
    }

    private void UpdateEnemy(Enemy enemy)
    {
        
    }
}