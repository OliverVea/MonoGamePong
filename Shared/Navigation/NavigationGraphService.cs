using Microsoft.Xna.Framework;
using Shared.Geometry.Definitions;
using Shared.Geometry.Helpers;
using Shared.Geometry.Shapes;

namespace Shared.Navigation;

public class NavigationGraphService
{
    public NavigationGraph BuildNavigationGraph(
        IReadOnlyList<NavigationNode> nodes,
        IEnumerable<LineSegment> levelGeometry,
        float cellSize,
        float maxEdgeDistance)
    {
        if (levelGeometry is not ICollection<LineSegment> levelGeometryCollection)
        {
            levelGeometryCollection = levelGeometry.ToArray();
        }
        
        var edges = new MatrixLookup<bool>(nodes.Count, nodes.Count);
        var weights = new MatrixLookup<float>(nodes.Count, nodes.Count);
        
        for (var i = 0; i < nodes.Count; i++)
        {
            for (var j = 0; j < nodes.Count; j++)
            {
                if (i == j)
                {
                    edges[i, j] = false;
                    weights[i, j] = 0;
                    
                    continue;
                }
                
                var a = nodes[i];
                var b = nodes[j];
                
                var distance = Vector2.Distance(a.Position, b.Position);
                
                if (distance >= cellSize * maxEdgeDistance)
                {
                    edges[i, j] = false;
                    weights[i, j] = 0;
                    
                    continue;
                }
                
                var lineSegment = new LineSegment(a.Position, b.Position);
                var intersects = levelGeometryCollection.Any(x => GeometryHelper.Overlaps(x, lineSegment));
                
                edges[i, j] = !intersects;
                weights[i, j] = distance;
            }
        }
        
        return new NavigationGraph
        {
            Nodes = nodes,
            Edges = edges,
            Weights = weights
        };
    }

    public IEnumerable<NavigationNode> SampleNavigationNodes(
        ICollection<LineSegment> levelGeometry,
        IReadOnlyCollection<ShapeInput> areas,
        float cellSize,
        int padding = 2)
    {
        var levelGeometryVertices = levelGeometry.SelectMany(x => new[] {x.Start, x.End}).Distinct().ToArray();
        
        var top = levelGeometryVertices.Max(x => x.Y) + cellSize * padding;
        var bottom = levelGeometryVertices.Min(x => x.Y) - cellSize * padding;
        var left = levelGeometryVertices.Min(x => x.X) - cellSize * padding;
        var right = levelGeometryVertices.Max(x => x.X) + cellSize * padding;
        
        var rows = (int) Math.Ceiling((top - bottom) / cellSize);
        var columns = (int) Math.Ceiling((right - left) / cellSize);
        
        var positions = new List<Vector2>(rows * (columns + 1));
        
        for (var i = 0; i < rows; i++)
        {
            var y = bottom + (i + 0.5f) * cellSize;
            for (var j = 0; j < columns + 1; j++)
            {
                const float offset = 0f;
                
                var x = left + (j + offset) * cellSize;
                
                var position = new Vector2(x, y);

                if (levelGeometry.Any(ls => GeometryHelper.Overlaps(ls, position))) continue;
                if (!areas.Any(area => area.Contains(position))) continue;

                positions.Add(new Vector2(x, y));
            }
        }
        
        return positions.Select(x => new NavigationNode {Position = x});
    }
}