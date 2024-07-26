namespace Shared.Navigation;

public class PathfindingService
{
    public OneOf<NavigationPath, NotFound> FindPath(NavigationGraph graph, Id<NavigationNode> start, Id<NavigationNode> end)
    {
        var startIndex = Array.FindIndex(graph.Nodes, node => node.Id == start);
        var endIndex = Array.FindIndex(graph.Nodes, node => node.Id == end);

        var frontier = new PriorityQueue<int[], float>();
        frontier.Enqueue([startIndex], 0);
        
        var visited = new HashSet<int>();
        
        int[]? path = null;
        
        while (frontier.TryDequeue(out var current, out var priority))
        {
            if (visited.Contains(current.Last())) continue;
            
            if (current.Last() == endIndex)
            {
                path = current;
                break;
            }
            
            for (var i = 0; i < graph.Nodes.Length; i++)
            {
                if (graph.Edges[current.Last(), i])
                {
                    var newCost = graph.Weights[current.Last(), i] + priority;
                    var newPath = current.Append(i).ToArray();
                    
                    frontier.Enqueue(newPath, newCost);
                }
            }
            
            visited.Add(current.Last());
        }
        
        if (path == null) return new NotFound();
        
        return new NavigationPath
        {
            Graph = graph,
            Nodes = path.Select(i => graph.Nodes[i]).ToArray()
        };
    }
}