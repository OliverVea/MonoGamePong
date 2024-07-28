using System;
using System.Linq;
using Entombed.Code.Game.Characters.Players;
using Microsoft.Xna.Framework;
using Shared.Lifetime;

namespace Entombed.Code.Game.Characters.Enemies;

public class EnemyUpdateService(Player player, CharacterLookup characterLookup) : IUpdateService
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

    private void SeekPlayer(Enemy enemy)
    {
        var delta = player.Position - enemy.Position;
        var distance = delta.Length();
        
        var direction = Vector2.Normalize(delta);
        
    }

    private void FightPlayer(Enemy enemy)
    {
        throw new NotImplementedException();
    }
}