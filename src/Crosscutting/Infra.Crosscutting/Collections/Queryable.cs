using System.Linq;
using System.Linq.Expressions;

namespace Goal.Infra.Crosscutting.Collections;

public static class Queryable
{
    public static TQueryable OrderingHelper<TQueryable, T>(IQueryable<T> source, string fieldName, SortDirection direction, bool anotherLevel)
        where TQueryable : IQueryable<T>
    {
        ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);
        MemberExpression property = null!;
        LambdaExpression sort = null!;

        foreach (string propName in fieldName.Split('.'))
        {
            property = Expression.Property((Expression)property ?? param, propName);
            sort = Expression.Lambda(property, param);
        }

        string level = !anotherLevel ? "OrderBy" : "ThenBy";
        string sortDirection = direction == SortDirection.Desc ? "Descending" : string.Empty;
        string methodName = $"{level}{sortDirection}";

        MethodCallExpression call = Expression.Call(
            typeof(System.Linq.Queryable),
            methodName,
            [typeof(T), property.Type],
            source.Expression,
            Expression.Quote(sort));

        return (TQueryable)source.Provider.CreateQuery<T>(call);
    }
}
