using Application.Interfaces;

namespace Application.Extensions;

public static class EnumerableExtensions
{
    public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (T element in enumerable)
        {
            action.Invoke(element);
        }
    }
    
    public static async Task EachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
    {
        foreach (T element in enumerable)
        {
            await action.Invoke(element);
        }
    }

    public static bool Exists<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
    {
        return enumerable.Any(predicate);
    }

    public static bool ContainsId<S, T>(this IEnumerable<S> enumerable, IEntity<T> item) where S : IEntity<S> where T : IEntity<T>
    {
        return enumerable.Any(e => e.Id == item.Id);
    }

    public static IEnumerable<T> AsEnumerable<T>(this T obj)
    {
        return new[] { obj };
    }
}