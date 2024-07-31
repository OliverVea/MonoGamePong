using Entombed.Code.Game.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using Shared.Camera;
using Shared.Navigation;

namespace Entombed.Code.Game.Navigation;

public class NavigationGuiService(GuiState guiState, NavigationState navigationState, IsometricCamera isometricCamera)
{
    private const float NodeRadius = 0.1f;
    
    public void DrawGui(SpriteBatch spriteBatch)
    {
        if (!guiState.ShowNavigationGraph) return;
        
        var navigationGraph = navigationState.NavigationGraph.Value;

        for (var i = 0; i < navigationGraph.Nodes.Length; i++)
        {
            DrawNode(spriteBatch, navigationGraph.Nodes[i]);
            DrawEdges(spriteBatch, navigationGraph, i);
        }
    }

    private void DrawNode(SpriteBatch spriteBatch, NavigationNode node)
    {
        var screenSpacePosition = isometricCamera.WorldToScreen(node.Position);
        var screenSpaceRadius = isometricCamera.WorldToScreen(NodeRadius);
        
        spriteBatch.DrawCircle(screenSpacePosition, screenSpaceRadius, 32, Color.White);
    }

    private void DrawEdges(SpriteBatch spriteBatch, NavigationGraph navigationGraph, int i)
    {
        for (var j = 0; j < navigationGraph.Nodes.Length; j++)
        {
            if (i == j) continue;
            
            var hasEdge = navigationGraph.Edges[i, j];
            if (!hasEdge) continue;
            
            var start = isometricCamera.WorldToScreen(navigationGraph.Nodes[i].Position);
            var end = isometricCamera.WorldToScreen(navigationGraph.Nodes[j].Position);
            
            spriteBatch.DrawLine(start, end, Color.White);
        }
    }
}