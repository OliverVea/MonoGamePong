using System.Collections.Generic;
using System.Linq;
using Entombed.Game.Gui;
using Entombed.Game.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Camera;

namespace Entombed.Game.Characters.Enemies;

public class EnemyGuiService(Level level, GuiState guiState, CharacterLookup characterLookup, IsometricCamera isometricCamera)
{
    public void DrawGui(SpriteBatch spriteBatch)
    {
        if (!guiState.ShowNavigationGraph) return;
        
        foreach (var enemy in characterLookup.Values.OfType<Enemy>())
        {
            if (enemy.LastGoal != EnemyGoal.ChaseGoal) continue;
            
            DrawEnemyNavigationPath(spriteBatch, enemy);
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
}