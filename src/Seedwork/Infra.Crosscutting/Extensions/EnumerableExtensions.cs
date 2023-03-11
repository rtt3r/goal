using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Seedwork.Infra.Crosscutting.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable ForEach(this IEnumerable source, Action<object> action)
        {
            Ensure.Argument.NotNull(source, nameof(source));
            Ensure.Argument.NotNull(action, nameof(action));

            foreach (object item in source)
            {
                action(item);
            }

            return source;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Ensure.Argument.NotNull(source, nameof(source));
            Ensure.Argument.NotNull(action, nameof(action));

            foreach (T element in source)
            {
                action(element);
            }

            return source;
        }

        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> values, IPageSearch pageSearch)
            => values.AsQueryable().Paginate(pageSearch);

        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, IPageSearch pageSearch)
        {
            Ensure.Argument.NotNull(pageSearch, nameof(pageSearch));

            IQueryable<T> queryableList = source;

            if (!string.IsNullOrWhiteSpace(pageSearch.SortBy))
            {
                queryableList = queryableList.OrderBy(pageSearch.SortBy, pageSearch.SortDirection);
            }

            queryableList = queryableList.Skip(pageSearch.PageIndex * pageSearch.PageSize);
            queryableList = queryableList.Take(pageSearch.PageSize);

            return queryableList;
        }

        public static async Task<IEnumerable<T>> PaginateAsync<T>(this IEnumerable<T> values, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
            => await values.AsQueryable().PaginateAsync(pageSearch, cancellationToken);

        public static async Task<IQueryable<T>> PaginateAsync<T>(this IQueryable<T> dataList, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Task.FromResult(dataList.Paginate(pageSearch));
        }

        public static IPagedCollection<T> PaginateList<T>(this IEnumerable<T> values, IPageSearch pageSearch)
            => values.AsQueryable().PaginateList(pageSearch);

        public static IPagedCollection<T> PaginateList<T>(this IQueryable<T> dataList, IPageSearch pageSearch)
        {
            Ensure.Argument.NotNull(pageSearch, nameof(pageSearch));
            return new PagedList<T>(dataList.Paginate(pageSearch).ToList(), dataList.Count());
        }

        public static async Task<IPagedCollection<T>> PaginateListAsync<T>(this IEnumerable<T> values, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
            => await values.AsQueryable().PaginateListAsync(pageSearch, cancellationToken);

        public static async Task<IPagedCollection<T>> PaginateListAsync<T>(this IQueryable<T> dataList, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Task.FromResult(dataList.PaginateList(pageSearch));
        }

        public static IPagedCollection<TResult> SelectPaged<TSource, TResult>(this IPagedCollection<TSource> source, Func<TSource, TResult> selector)
        {
            IEnumerable<TResult> items = source.Select(selector);
            return new PagedList<TResult>(items, source.TotalCount);
        }

        public static string Join(this IEnumerable<string> values, string separator) => string.Join(separator, values);

        public static string Join<T>(this IEnumerable<T> values, string separator) => string.Join(separator, values);
    }
}
