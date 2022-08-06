using System.Collections.Generic;
using Goal.Seedwork.Infra.Crosscutting.Adapters;
using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Seedwork.Application.Extensions
{
    public static class ProjectionsExtensionMethods
    {
        public static TProjection ProjectAs<TProjection>(this ITypeAdapter adapter, object source)
            where TProjection : class, new() => adapter.Adapt<TProjection>(source);

        public static TProjection ProjectAs<TSource, TProjection>(this ITypeAdapter adapter, TSource source)
            where TSource : class
            where TProjection : class, new() => adapter.ProjectAs<TProjection>(source);

        public static ICollection<TProjection> ProjectAsCollection<TProjection>(this ITypeAdapter adapter, IEnumerable<object> source)
            where TProjection : class, new() => adapter.Adapt<List<TProjection>>(source);

        public static ICollection<TProjection> ProjectAsCollection<TSource, TProjection>(this ITypeAdapter adapter, IEnumerable<TSource> source)
            where TSource : class
            where TProjection : class, new() => adapter.ProjectAsCollection<TProjection>(source);

        public static IPagedCollection<TProjection> ProjectAsPagedCollection<TProjection>(this ITypeAdapter adapter, IPagedCollection<object> source)
            where TProjection : class, new()
        {
            List<TProjection> projection = adapter.Adapt<List<TProjection>>(source);
            return new PagedList<TProjection>(projection, source.TotalCount);
        }

        public static IPagedCollection<TProjection> ProjectAsPagedCollection<TSource, TProjection>(this ITypeAdapter adapter, IPagedCollection<TSource> source)
            where TSource : class
            where TProjection : class, new() => adapter.ProjectAsPagedCollection<TProjection>(source);
    }
}
