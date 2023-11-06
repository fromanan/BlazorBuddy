namespace Application.Extensions;

public static class EnumerableExtensions
{
    public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (T element in enumerable)
        {
            action(element);
        }
    }
    
    public static async Task EachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
    {
        foreach (T element in enumerable)
        {
            await action(element);
        }
    }
}