using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Seedwork.Infra.Crosscutting.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable ForEach(this IEnumerable source, Action<object> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);

        foreach (object item in source)
        {
            action(item);
        }

        return source;
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);

        foreach (T element in source)
        {
            action(element);
        }

        return source;
    }

    public static IPagedList<TResult> SelectPaged<TSource, TResult>(this IPagedList<TSource> source, Func<TSource, TResult> selector)
    {
        IEnumerable<TResult> items = source.Select(selector);
        return new PagedList<TResult>(items, source.TotalCount);
    }

    public static string Join(this IEnumerable<string> values, string separator) => string.Join(separator, values);

    public static string Join<T>(this IEnumerable<T> values, string separator) => string.Join(separator, values);
}
