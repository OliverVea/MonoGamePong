using System;
using Entombed.Game.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shared.Camera;
using Shared.Navigation;

namespace Entombed.Game.Navigation;

public class NavigationGuiService
{
    private const float NodeRadius = 0.025f;
    
    private readonly Texture2D _nodeTexture;
    private readonly GuiState _guiState;
    private readonly NavigationState _navigationState;
    private readonly IsometricCamera _isometricCamera;

    public NavigationGuiService(GraphicsDevice graphicsDevice, GuiState guiState, NavigationState navigationState, IsometricCamera isometricCamera)
    {
        _guiState = guiState;
        _navigationState = navigationState;
        _isometricCamera = isometricCamera;
        
        
        var screenSpaceRadius = (int)MathF.Ceiling(isometricCamera.WorldToScreen(NodeRadius));
        var screenSpaceDiameter = screenSpaceRadius * 2;
        _nodeTexture = new Texture2D(graphicsDevice, screenSpaceDiameter, screenSpaceDiameter);
        
        var data = new Color[screenSpaceDiameter * screenSpaceDiameter];
        
        for (var x = 0; x < screenSpaceDiameter; x++)
        {
            for (var y = 0; y < screenSpaceDiameter; y++)
            {
                var distance = MathF.Sqrt(MathF.Pow(x - screenSpaceRadius, 2) + MathF.Pow(y - screenSpaceRadius, 2));
                var alpha = distance > screenSpaceRadius ? 0 : 1;
                
                data[x + y * screenSpaceDiameter] = new Color(255, 255, 255, alpha);
            }
        }
        
        _nodeTexture.SetData(data);
    }

    public void DrawGui(SpriteBatch spriteBatch)
    {
        if (!_guiState.ShowNavigationGraph) return;
        
        var navigationGraph = _navigationState.NavigationGraph;

        foreach (var node in navigationGraph.Nodes)
        {
            DrawNode(spriteBatch, node);
            //DrawEdges(spriteBatch, navigationGraph, i);
        }
    }

    private void DrawNode(SpriteBatch spriteBatch, NavigationNode node)
    {
        var screenSpacePosition = _isometricCamera.WorldToScreen(node.Position) - new Vector2(_nodeTexture.Width / 2f, _nodeTexture.Height / 2f);
        
        spriteBatch.Draw(_nodeTexture, screenSpacePosition, Color.White);
    }

    /*
    private void DrawEdges(SpriteBatch spriteBatch, NavigationGraph navigationGraph, int i)
    {
        for (var j = 0; j < navigationGraph.Nodes.Length; j++)
        {
            if (i == j) continue;
            
            var hasEdge = navigationGraph.Edges[i, j];
            if (!hasEdge) continue;
            
            var start = _isometricCamera.WorldToScreen(navigationGraph.Nodes[i].Position);
            var end = _isometricCamera.WorldToScreen(navigationGraph.Nodes[j].Position);
            
            spriteBatch.DrawLine(start, end, Color.White);
        }
    }
    */
}