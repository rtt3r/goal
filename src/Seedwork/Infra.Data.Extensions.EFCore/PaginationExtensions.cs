using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Crosscutting.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Goal.Seedwork.Infra.Data.Extensions.EFCore;

public static class PaginationExtensions
{
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(pageSearch, nameof(pageSearch));

        return new PagedList<T>(
            await source.Paginate(pageSearch).ToListAsync(cancellationToken),
            await source.CountAsync(cancellationToken));
    }
}
