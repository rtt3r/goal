using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace Goal.Seedwork.Infra.Data.Extensions.RavenDB;

public static class PaginationExtensions
{
    public static IRavenQueryable<T> Paginate<T>(this IRavenQueryable<T> source, IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch, nameof(pageSearch));

        IRavenQueryable<T> queryableList = source;

        if (!string.IsNullOrWhiteSpace(pageSearch.SortBy))
        {
            queryableList = queryableList.OrderBy(pageSearch.SortBy, pageSearch.SortDirection);
        }

        queryableList = queryableList.Skip(pageSearch.PageIndex * pageSearch.PageSize);
        queryableList = queryableList.Take(pageSearch.PageSize);

        return queryableList;
    }

    public static IPagedList<T> ToPagedList<T>(this IRavenQueryable<T> source, IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch, nameof(pageSearch));

        var data = source
            .Statistics(out QueryStatistics stats)
            .Paginate(pageSearch)
            .ToList();

        return new PagedList<T>(
            data,
            stats.TotalResults);
    }

    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IRavenQueryable<T> source, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(pageSearch, nameof(pageSearch));

        List<T> data = await source
            .Statistics(out QueryStatistics stats)
            .Paginate(pageSearch)
            .ToListAsync(cancellationToken);

        return new PagedList<T>(
            data,
            stats.TotalResults);
    }
}
