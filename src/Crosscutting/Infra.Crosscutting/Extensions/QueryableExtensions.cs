using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Infra.Crosscutting.Collections;
using GoalQueryable = Goal.Infra.Crosscutting.Collections.Queryable;

namespace Goal.Infra.Crosscutting.Extensions;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string fieldName, SortDirection direction)
        => GoalQueryable.OrderingHelper<IOrderedQueryable<T>, T>(source, fieldName, direction, false);

    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string fieldName)
        => GoalQueryable.OrderingHelper<IOrderedQueryable<T>, T>(source, fieldName, SortDirection.Asc, false);

    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string fieldName)
        => GoalQueryable.OrderingHelper<IOrderedQueryable<T>, T>(source, fieldName, SortDirection.Desc, false);

    public static IOrderedQueryable<T> ThenBy<T>(this IQueryable<T> source, string fieldName, SortDirection direction)
        => GoalQueryable.OrderingHelper<IOrderedQueryable<T>, T>(source, fieldName, direction, true);

    public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string fieldName)
        => GoalQueryable.OrderingHelper<IOrderedQueryable<T>, T>(source, fieldName, SortDirection.Asc, true);

    public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string fieldName)
        => GoalQueryable.OrderingHelper<IOrderedQueryable<T>, T>(source, fieldName, SortDirection.Desc, true);

    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch);

        IQueryable<T> queryableList = source;

        if (!string.IsNullOrWhiteSpace(pageSearch.SortBy))
        {
            queryableList = queryableList.OrderBy(pageSearch.SortBy, pageSearch.SortDirection);
        }

        queryableList = queryableList.Skip(pageSearch.PageIndex * pageSearch.PageSize);
        queryableList = queryableList.Take(pageSearch.PageSize);

        return queryableList;
    }

    public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch);

        return new PagedList<T>(
            source.Paginate(pageSearch).ToList(),
            source.Count());
    }
}
