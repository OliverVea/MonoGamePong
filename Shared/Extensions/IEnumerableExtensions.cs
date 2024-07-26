namespace Shared.Extensions;

// ReSharper disable once InconsistentNaming
public static class IEnumerableExtensions
{
    public static T PickOne<T>(this IEnumerable<T> enumerable)
    {
        var random = new Random();
        return enumerable.OrderBy(_ => random.Next()).First();
    }
}