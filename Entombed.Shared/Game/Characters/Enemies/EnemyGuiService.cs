using System.Collections.Generic;
using System.Linq;
using Entombed.Game.Characters.Players;
using Entombed.Game.Gui;
using Entombed.Game.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Camera;

namespace Entombed.Game.Characters.Enemies;

public class EnemyGuiService(Player player, Level level, GuiState guiState, CharacterLookup characterLookup, IsometricCamera isometricCamera)
{
    public void DrawGui(SpriteBatch spriteBatch)
    {
        if (!guiState.ShowNavigationGraph) return;
        
        var enemyByLastGoal = characterLookup.Values.OfType<Enemy>().GroupBy(x => x.LastGoal).ToDictionary(x => x.Key, x => x.ToList());
        
        foreach (var enemiesWithGoal in enemyByLastGoal)
        {
            DrawEnemiesWithGoal(spriteBatch, enemiesWithGoal.Key, enemiesWithGoal.Value);
        }
        
        foreach (var enemy in characterLookup.Values.OfType<Enemy>())
        {
            if (enemy.LastGoal != EnemyGoal.ChaseGoal) continue;
            
            DrawEnemyNavigationPath(spriteBatch, enemy);
        }
    }

    private void DrawEnemiesWithGoal(SpriteBatch spriteBatch, EnemyGoal goal, List<Enemy> enemies)
    {
        switch (goal)
        {
            case EnemyGoal.None:
                break;
            case EnemyGoal.FightPlayer:
                foreach (var enemy in enemies) DrawEnemyPlayerPath(spriteBatch, enemy);
                break;
            case EnemyGoal.FightGoal:
                foreach (var enemy in enemies) DrawEnemyGoalPath(spriteBatch, enemy);
                break;
            case EnemyGoal.ChaseGoal:
                foreach (var enemy in enemies) DrawEnemyNavigationPath(spriteBatch, enemy);
                break;
            case EnemyGoal.Idle:
                foreach (var enemy in enemies) DrawEnemyIdlePath(spriteBatch, enemy);
                break;
            case EnemyGoal.Skipped:
            case EnemyGoal.ChasePlayer:
            default:
                break;
        }
    }

    private void DrawEnemyNavigationPath(SpriteBatch spriteBatch, Enemy enemy)
    {
        if (enemy.NavigationState == null) return;

        List<Vector2> nodes =
        [
            enemy.Position,
        ];
        
        nodes.AddRange(enemy.NavigationState.Path.Nodes.Skip(enemy.NavigationState.PathIndex).Select(node => node.Position));
        
        nodes.Add(level.Goal);
        
        for (var i = 0; i < nodes.Count - 1; i++)
        {
            var start = isometricCamera.WorldToScreen(nodes[i]);
            var end = isometricCamera.WorldToScreen(nodes[i + 1]);
            
            spriteBatch.DrawLine(start, end, Color.Red);
        }
    }
    
    private void DrawEnemyPlayerPath(SpriteBatch spriteBatch, Enemy enemy)
    {
        var start = isometricCamera.WorldToScreen(enemy.Position);
        var end = isometricCamera.WorldToScreen(player.Position);
        
        spriteBatch.DrawLine(start, end, Color.Red);
    }
    
    private void DrawEnemyGoalPath(SpriteBatch spriteBatch, Enemy enemy)
    {
        var start = isometricCamera.WorldToScreen(enemy.Position);
        var end = isometricCamera.WorldToScreen(level.Goal);
        
        spriteBatch.DrawLine(start, end, Color.Red);
    }
    
    private void DrawEnemyIdlePath(SpriteBatch spriteBatch, Enemy enemy)
    {
        if (enemy.IdleTarget is not {} idleTarget) return;
        
        var start = isometricCamera.WorldToScreen(enemy.Position);
        var end = isometricCamera.WorldToScreen(idleTarget);
        
        spriteBatch.DrawLine(start, end, Color.Red);
    }
}