using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infra.Data.Extensions;

public static class PaginationExtensions
{
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(pageSearch);

        return await source.ToPagedListAsync(
            pageSearch.PageIndex,
            pageSearch.PageSize,
            cancellationToken);
    }

    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentOutOfRangeException.ThrowIfNegative(pageIndex);
        ArgumentOutOfRangeException.ThrowIfNegative(pageSize);

        List<T> items = await source.Paginate(pageIndex, pageSize).ToListAsync(cancellationToken);
        int totalCount = await source.CountAsync(cancellationToken);

        return new PagedList<T>(items, totalCount);
    }
}