using System.Collections.Generic;
using Goal.Seedwork.Infra.Crosscutting.Adapters;
using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Application.Extensions;

public static class ProjectionsExtensionMethods
{
    public static ICollection<TTarget> AdaptList<TTarget>(this ITypeAdapter adapter, IEnumerable<object> source)
        => adapter.Adapt<List<TTarget>>(source);

    public static ICollection<TTarget> AdaptList<TSource, TTarget>(this ITypeAdapter adapter, IEnumerable<TSource> source)
        => adapter.Adapt<IEnumerable<TSource>, List<TTarget>>(source);

    public static IPagedList<TTarget> AdaptPagedList<TTarget>(this ITypeAdapter adapter, IPagedList<object> source)
    {
        List<TTarget> projection = adapter.Adapt<List<TTarget>>(source);
        return new PagedList<TTarget>(projection, source.TotalCount);
    }

    public static IPagedList<TTarget> AdaptPagedList<TSource, TTarget>(this ITypeAdapter adapter, IPagedList<TSource> source)
    {
        List<TTarget> projection = adapter.Adapt<IPagedList<TSource>, List<TTarget>>(source);
        return new PagedList<TTarget>(projection, source.TotalCount);
    }
}
