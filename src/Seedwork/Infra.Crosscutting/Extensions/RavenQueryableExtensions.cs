using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace Goal.Seedwork.Infra.Crosscutting.Extensions
{
    public static class RavenQueryableExtensions
    {
        public static IPagedCollection<T> PaginateList<T>(this IRavenQueryable<T> source, IPageSearch pageSearch)
        {
            Ensure.Argument.NotNull(pageSearch, nameof(pageSearch));

            IQueryable<T> query = source
                 .Statistics(out QueryStatistics stats)
                 .Paginate(pageSearch);

            return new PagedList<T>(query.ToList(), stats.TotalResults);
        }

        public static async Task<IPagedCollection<T>> PaginateListAsync<T>(this IRavenQueryable<T> source, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken())
        {
            Ensure.Argument.NotNull(pageSearch, nameof(pageSearch));

            IQueryable<T> query = source
                .Statistics(out QueryStatistics stats)
                .Paginate(pageSearch);

            return new PagedList<T>(
                await query.ToListAsync(cancellationToken),
                stats.TotalResults);
        }
    }
}
