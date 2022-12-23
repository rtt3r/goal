using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Seedwork.Infra.Crosscutting.Extensions
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string fieldName, SortDirection direction)
            => OrderingHelper(source, fieldName, direction, false);

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string fieldName)
            => OrderingHelper(source, fieldName, SortDirection.Asc, false);

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string fieldName)
            => OrderingHelper(source, fieldName, SortDirection.Desc, false);

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string fieldName)
            => OrderingHelper(source, fieldName, SortDirection.Asc, true);

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string fieldName)
            => OrderingHelper(source, fieldName, SortDirection.Desc, true);

        private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string fieldName, SortDirection direction, bool anotherLevel)
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

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
    }
}
