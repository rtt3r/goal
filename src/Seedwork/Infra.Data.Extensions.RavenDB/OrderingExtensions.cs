using Goal.Seedwork.Infra.Crosscutting.Collections;
using Raven.Client.Documents.Linq;
using GoalQueryable = Goal.Seedwork.Infra.Crosscutting.Collections.Queryable;

namespace Goal.Infra.Data.Extensions.RavenDB;

public static class OrderingExtensions
{
    public static IRavenQueryable<T> OrderBy<T>(this IRavenQueryable<T> source, string fieldName, SortDirection direction)
        => GoalQueryable.OrderingHelper<IRavenQueryable<T>, T>(source, fieldName, direction, false);

    public static IRavenQueryable<T> OrderBy<T>(this IRavenQueryable<T> source, string fieldName)
        => GoalQueryable.OrderingHelper<IRavenQueryable<T>, T>(source, fieldName, SortDirection.Asc, false);

    public static IRavenQueryable<T> OrderByDescending<T>(this IRavenQueryable<T> source, string fieldName)
        => GoalQueryable.OrderingHelper<IRavenQueryable<T>, T>(source, fieldName, SortDirection.Desc, false);

    public static IRavenQueryable<T> ThenBy<T>(this IRavenQueryable<T> source, string fieldName, SortDirection direction)
        => GoalQueryable.OrderingHelper<IRavenQueryable<T>, T>(source, fieldName, direction, true);

    public static IRavenQueryable<T> ThenBy<T>(this IRavenQueryable<T> source, string fieldName)
        => GoalQueryable.OrderingHelper<IRavenQueryable<T>, T>(source, fieldName, SortDirection.Asc, true);

    public static IRavenQueryable<T> ThenByDescending<T>(this IRavenQueryable<T> source, string fieldName)
        => GoalQueryable.OrderingHelper<IRavenQueryable<T>, T>(source, fieldName, SortDirection.Desc, true);
}
