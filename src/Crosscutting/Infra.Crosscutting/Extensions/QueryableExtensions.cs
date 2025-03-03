using System;
using System.Linq;
using Goal.Infra.Crosscutting.Collections;

namespace Goal.Infra.Crosscutting.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch);
        ArgumentOutOfRangeException.ThrowIfNegative(pageSearch.PageIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(pageSearch.PageSize);

        source = source.Skip(pageSearch.PageIndex * pageSearch.PageSize);
        source = source.Take(pageSearch.PageSize);

        return source;
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageIndex, int pageSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(pageIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(pageSize);

        source = source.Skip(pageIndex * pageSize);
        source = source.Take(pageSize);

        return source;
    }

    public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch);
        ArgumentOutOfRangeException.ThrowIfNegative(pageSearch.PageIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(pageSearch.PageSize);

        return new PagedList<T>(
            [.. source.Paginate(pageSearch)],
            source.Count());
    }

    public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(pageIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(pageSize);

        return new PagedList<T>(
            [.. source.Paginate(pageIndex, pageSize)],
            source.Count());
    }
}
