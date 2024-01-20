using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infra.Data.EntityFrameworkCore;

public static class PaginationExtensions
{
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(pageSearch);

        return new PagedList<T>(
            await source.Paginate(pageSearch).ToListAsync(cancellationToken),
            await source.CountAsync(cancellationToken));
    }
}
