using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Goal.Infra.Crosscutting.Collections;

namespace Goal.Infra.Data.Query;

public interface IQueryRepository<TEntity, TKey> : IQueryRepository
    where TEntity : class
{
    Task<TEntity?> LoadAsync(TKey id, CancellationToken cancellationToken = new CancellationToken());
    Task<ICollection<TEntity>> QueryAsync(CancellationToken cancellationToken = new CancellationToken());
    Task<IPagedList<TEntity>> QueryAsync(IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken());
    Task StoreAsync(TKey id, TEntity entity, CancellationToken cancellationToken = new CancellationToken());
    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken());
    Task RemoveAsync(TKey id, CancellationToken cancellationToken = new CancellationToken());
}
