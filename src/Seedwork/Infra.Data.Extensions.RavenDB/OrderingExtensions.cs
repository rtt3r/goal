using System.Linq;
using System.Linq.Expressions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Raven.Client.Documents.Linq;

namespace Goal.Seedwork.Infra.Data.Extensions.RavenDB;

public static class OrderingExtensions
{
    public static IRavenQueryable<T> OrderBy<T>(this IRavenQueryable<T> source, string fieldName, SortDirection direction)
        => OrderingHelper(source, fieldName, direction, false);

    public static IRavenQueryable<T> OrderBy<T>(this IRavenQueryable<T> source, string fieldName)
        => OrderingHelper(source, fieldName, SortDirection.Asc, false);

    public static IRavenQueryable<T> OrderByDescending<T>(this IRavenQueryable<T> source, string fieldName)
        => OrderingHelper(source, fieldName, SortDirection.Desc, false);

    public static IRavenQueryable<T> ThenBy<T>(this IRavenQueryable<T> source, string fieldName)
        => OrderingHelper(source, fieldName, SortDirection.Asc, true);

    public static IRavenQueryable<T> ThenByDescending<T>(this IRavenQueryable<T> source, string fieldName)
        => OrderingHelper(source, fieldName, SortDirection.Desc, true);

    private static IRavenQueryable<T> OrderingHelper<T>(IRavenQueryable<T> source, string fieldName, SortDirection direction, bool anotherLevel)
    {
        ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);
        MemberExpression property = null;
        LambdaExpression sort = null;

        foreach (string propName in fieldName.Split('.'))
        {
            property = Expression.Property((Expression)property ?? param, propName);
            sort = Expression.Lambda(property, param);
        }

        string level = !anotherLevel ? "OrderBy" : "ThenBy";
        string sortDirection = direction == SortDirection.Desc ? "Descending" : string.Empty;

        MethodCallExpression call = Expression.Call(
            typeof(Queryable),
            $"{level}{sortDirection}",
            new[] { typeof(T), property?.Type },
            source.Expression,
            Expression.Quote(sort));

        return (IRavenQueryable<T>)source.Provider.CreateQuery<T>(call);
    }
}
