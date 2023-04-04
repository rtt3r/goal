using System.Linq;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using GoalQueryable = Goal.Seedwork.Infra.Crosscutting.Collections.Queryable;

namespace Goal.Seedwork.Infra.Crosscutting.Extensions;

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
        Ensure.Argument.IsNotNull(pageSearch, nameof(pageSearch));

        IQueryable<T> queryableList = source;

        if (!string.IsNullOrWhiteSpace(pageSearch.SortBy))
        {
            queryableList = queryableList.OrderBy(pageSearch.SortBy, pageSearch.SortDirection);
        }

        queryableList = queryableList.Skip(pageSearch.PageIndex * pageSearch.PageSize);
        queryableList = queryableList.Take(pageSearch.PageSize);

        return queryableList;
    }

    public static IPagedCollection<T> ToPagedList<T>(this IQueryable<T> source, IPageSearch pageSearch)
    {
        Ensure.Argument.IsNotNull(pageSearch, nameof(pageSearch));

        return new PagedList<T>(
            source.Paginate(pageSearch).ToList(),
            source.Count());
    }
}
