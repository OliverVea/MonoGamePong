using Microsoft.Xna.Framework;

namespace Shared.Navigation;

public class PathfindingService
{
    public OneOf<NavigationPath, NotFound> FindPath(NavigationGraph graph, Id<NavigationNode> start, Id<NavigationNode> end)
    {
        var startIndex = FindIndex(graph.Nodes, node => node.Id == start);
        var endIndex = FindIndex(graph.Nodes, node => node.Id == end);
        
        var frontier = new PriorityQueue<Path, float>();
        
        var firstPath = new Path { Nodes = [startIndex], Cost = 0 };
        
        frontier.Enqueue(firstPath, Heuristic(startIndex, endIndex));
        
        var visited = new HashSet<int>();
        
        Path? path = null;
        
        while (frontier.TryDequeue(out var current, out var priority))
        {
            if (visited.Contains(current.Nodes.Last())) continue;
            
            if (current.Nodes.Last() == endIndex)
            {
                path = current;
                break;
            }
            
            for (var i = 0; i < graph.Nodes.Count; i++)
            {
                if (graph.Edges[current.Nodes.Last(), i])
                {
                    var newCost = graph.Weights[current.Nodes.Last(), i] + current.Cost;
                    var newNodes = current.Nodes.Append(i).ToArray();
                    
                    var newPath = new Path { Nodes = newNodes, Cost = newCost };
                    
                    frontier.Enqueue(newPath, newCost + Heuristic(i, endIndex));
                }
            }
            
            visited.Add(current.Nodes.Last());
        }
        
        if (!path.HasValue) return new NotFound();

        var nodeQueue = path.Value.Nodes.Select(i => graph.Nodes[i]).ToArray();
        
        return new NavigationPath
        {
            Graph = graph,
            Nodes = nodeQueue
        };
        
        float Heuristic(int indexA, int indexB) => Vector2.Distance(graph.Nodes[indexA].Position, graph.Nodes[indexB].Position);
    }

    private readonly struct Path
    {
        public required int[] Nodes { get; init; }
        public required float Cost { get; init; }
    }

    private int FindIndex<T>(IReadOnlyList<T> list, Func<T, bool> predicate)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (predicate(list[i])) return i;
        }
        
        return -1;
    }
}