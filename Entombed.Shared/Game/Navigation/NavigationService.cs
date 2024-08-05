using System.Linq;
using Microsoft.Xna.Framework;
using Shared.Navigation;

namespace Entombed.Game.Navigation;

public class NavigationService(NavigationState navigationState, PathfindingService pathfindingService)
{

    public OneOf<NavigationPath, NotFound> FindPath(Vector2 enemyPosition, Vector2 levelGoal)
    {
        var startNode = FindClosestNode(enemyPosition);
        if (startNode is null) return new NotFound();
        
        var goalNode = FindClosestNode(levelGoal);
        if (goalNode is null) return new NotFound();
        
        return pathfindingService.FindPath(navigationState.NavigationGraph.Value, startNode.Id, goalNode.Id);
    }

    private NavigationNode? FindClosestNode(Vector2 position)
    {
        return navigationState.NavigationGraph.Value.Nodes.MinBy(node => Vector2.Distance(node.Position, position));
    }
}