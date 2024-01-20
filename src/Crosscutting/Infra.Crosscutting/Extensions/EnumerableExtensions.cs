using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Goal.Infra.Crosscutting.Collections;

namespace Goal.Infra.Crosscutting.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable ForEach(this IEnumerable source, Action<object> body)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(body);

        List<Exception>? exceptions = null;

        foreach (object item in source)
        {
            try
            {
                body(item);
            }
            catch (Exception exc)
            {
                exceptions ??= [];
                exceptions.Add(exc);
            }
        }

        return exceptions != null
            ? throw new AggregateException(exceptions)
            : source;
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> body)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(body);

        List<Exception>? exceptions = null;

        foreach (T item in source)
        {
            try
            {
                body(item);
            }
            catch (Exception exc)
            {
                exceptions ??= [];
                exceptions.Add(exc);
            }
        }

        return exceptions != null
            ? throw new AggregateException(exceptions)
            : source;
    }

    public static IPagedList<TResult> SelectPaged<TSource, TResult>(this IPagedList<TSource> source, Func<TSource, TResult> selector)
    {
        IEnumerable<TResult> items = source.Select(selector);
        return new PagedList<TResult>(items, source.TotalCount);
    }
}
