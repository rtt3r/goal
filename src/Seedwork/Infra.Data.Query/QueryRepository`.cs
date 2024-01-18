using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Infra.Data.Query;

public abstract class QueryRepository<TEntity, TKey> : QueryRepository, IQueryRepository<TEntity, TKey>
    where TEntity : class
{
    public abstract Task<TEntity> LoadAsync(TKey id, CancellationToken cancellationToken = new CancellationToken());
    public abstract Task<ICollection<TEntity>> QueryAsync(CancellationToken cancellationToken = new CancellationToken());
    public abstract Task<IPagedList<TEntity>> QueryAsync(IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken());
    public abstract Task StoreAsync(TKey id, TEntity entity, CancellationToken cancellationToken = new CancellationToken());
    public abstract Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken());
    public abstract Task RemoveAsync(TKey id, CancellationToken cancellationToken = new CancellationToken());
}
