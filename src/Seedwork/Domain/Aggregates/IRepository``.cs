using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Crosscutting.Specifications;

namespace Goal.Seedwork.Domain.Aggregates;

public interface IRepository<TEntity, TKey> : IRepository
    where TEntity : class
{
    TEntity Load(TKey key);

    ICollection<TEntity> Query();

    ICollection<TEntity> Query(ISpecification<TEntity> specification);

    IPagedCollection<TEntity> Query(IPageSearch pageSearch);

    IPagedCollection<TEntity> Query(ISpecification<TEntity> specification, IPageSearch pageSearch);

    Task<TEntity> LoadAsync(TKey key, CancellationToken cancellationToken = new CancellationToken());

    Task<ICollection<TEntity>> QueryAsync(CancellationToken cancellationToken = new CancellationToken());

    Task<ICollection<TEntity>> QueryAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = new CancellationToken());

    Task<IPagedCollection<TEntity>> QueryAsync(IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken());

    Task<IPagedCollection<TEntity>> QueryAsync(ISpecification<TEntity> specification, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken());

    bool Any();

    bool Any(ISpecification<TEntity> specification);

    Task<bool> AnyAsync(CancellationToken cancellationToken = new CancellationToken());

    Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = new CancellationToken());

    void Add(TEntity entity);

    void Add(IEnumerable<TEntity> entities);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken());

    Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = new CancellationToken());

    void Update(TEntity entity);

    void Update(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void Remove(IEnumerable<TEntity> entities);

    void Remove(ISpecification<TEntity> specification);
}
