using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Infra.Crosscutting;
using Goal.Seedwork.Infra.Crosscutting.Collections;

/* Unmerged change from project 'Infra.Data.Extensions.RavenDB (net7.0)'
Before:
using Microsoft.EntityFrameworkCore;
After:
using Goal.Seedwork.Infra.Data.Extensions.RavenDB;
using Microsoft.EntityFrameworkCore;
*/
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;

namespace Goal.Seedwork.Infra.Data.Extensions.RavenDB;

public static class PaginationExtensions
{
    public static IRavenQueryable<T> Paginate<T>(this IRavenQueryable<T> source, IPageSearch pageSearch)
    {
        Ensure.Argument.IsNotNull(pageSearch, nameof(pageSearch));

        IRavenQueryable<T> queryableList = source;

        if (!string.IsNullOrWhiteSpace(pageSearch.SortBy))
        {
            queryableList = queryableList.OrderBy(pageSearch.SortBy, pageSearch.SortDirection);
        }

        queryableList = queryableList.Skip(pageSearch.PageIndex * pageSearch.PageSize);
        queryableList = queryableList.Take(pageSearch.PageSize);

        return queryableList;
    }

    public static IPagedCollection<T> ToPagedList<T>(this IRavenQueryable<T> dataList, IPageSearch pageSearch)
    {
        Ensure.Argument.IsNotNull(pageSearch, nameof(pageSearch));

        return new PagedList<T>(
            dataList.Paginate(pageSearch).ToList(),
            dataList.Count());
    }

    public static async Task<IPagedCollection<T>> ToPagedListAsync<T>(this IRavenQueryable<T> dataList, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
    {
        Ensure.Argument.IsNotNull(pageSearch, nameof(pageSearch));

        return new PagedList<T>(
            await dataList.Paginate(pageSearch).ToListAsync(cancellationToken),
            await dataList.CountAsync());
    }
}
