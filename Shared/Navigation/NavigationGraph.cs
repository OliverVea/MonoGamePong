using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;

namespace Shared.Navigation;

public class NavigationGraph
{
    public required IReadOnlyList<NavigationNode> Nodes { get; init; }
    public required MatrixLookup<bool> Edges { get; init; }
    public required MatrixLookup<float> Weights { get; init; }
}

public class NavigationNode
{
    public NavigationNode()
    {
    }
    
    [SetsRequiredMembers]
    public NavigationNode(Vector2 position)
    {
        Position = position;
    }
    
    public Id<NavigationNode> Id { get; } = Id<NavigationNode>.NewId();
    public required Vector2 Position { get; init; }
}

public class MatrixLookup<T>(int rows, int columns)
{
    private readonly T[] _data = new T[rows * columns];

    public T this[int i, int j]
    {
        get
        {
            if (i < 0 || i >= rows || j < 0 || j >= columns)
            {
                throw new IndexOutOfRangeException();
            }

            var min = int.Min(i, j);
            var max = int.Max(i, j);
        
            return _data[min * columns + max];
        }
        set
        {
            if (i < 0 || i >= rows || j < 0 || j >= columns)
            {
                throw new IndexOutOfRangeException();
            }

            var min = int.Min(i, j);
            var max = int.Max(i, j);
        
            _data[min * columns + max] = value;
        }
    }
}