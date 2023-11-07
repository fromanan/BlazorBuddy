using Application.Interfaces;

namespace Application.Extensions;

public static class ModelExtensions
{
    public static IEnumerable<T> Copy<T>(this IEnumerable<T> entities) where T : IEntity<T>
    {
        return entities.Select(e => e.Copy());
    }
}