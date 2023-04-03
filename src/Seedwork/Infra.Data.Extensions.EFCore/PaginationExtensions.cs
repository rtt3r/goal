using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Infra.Crosscutting;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Crosscutting.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Goal.Seedwork.Infra.Data.Extensions.EFCore;

public static class PaginationExtensions
{
    public static async Task<IPagedCollection<T>> ToPagedListAsync<T>(this IQueryable<T> dataList, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
    {
        Ensure.Argument.NotNull(pageSearch, nameof(pageSearch));

        return new PagedList<T>(
            await dataList.Paginate(pageSearch).ToListAsync(cancellationToken),
            await dataList.CountAsync());
    }
}
