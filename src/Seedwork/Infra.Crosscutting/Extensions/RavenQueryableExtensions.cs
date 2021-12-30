using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Infra.Crosscutting.Collections;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace Goal.Infra.Crosscutting.Extensions
{
    public static class RavenQueryableExtensions
    {
        public static IPagedCollection<T> PaginateList<T>(this IRavenQueryable<T> source, IPagination page)
        {
            Ensure.Argument.NotNull(page, nameof(page));

            IQueryable<T> query = source
                 .Statistics(out QueryStatistics stats)
                 .Paginate(page);

            return new PagedList<T>(query.ToList(), stats.TotalResults);
        }

        public static async Task<IPagedCollection<T>> PaginateListAsync<T>(this IRavenQueryable<T> source, IPagination page, CancellationToken cancellationToken = new CancellationToken())
        {
            Ensure.Argument.NotNull(page, nameof(page));

            IQueryable<T> query = source
                .Statistics(out QueryStatistics stats)
                .Paginate(page);

            return new PagedList<T>(
                await query.ToListAsync(cancellationToken),
                stats.TotalResults);
        }
    }
}
