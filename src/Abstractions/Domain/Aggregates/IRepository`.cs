using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Specifications;

namespace Goal.Domain.Aggregates;

public interface IRepository<TEntity, TKey> : IRepository
    where TEntity : class
{
    TEntity? Get(TKey key);

    ICollection<TEntity> List();

    ICollection<TEntity> Search(ISpecification<TEntity> specification);

    IPagedList<TEntity> Search(IPageSearch pageSearch);

    IPagedList<TEntity> Search(ISpecification<TEntity> specification, IPageSearch pageSearch);

    Task<TEntity?> GetAsync(TKey key, CancellationToken cancellationToken = new CancellationToken());

    Task<ICollection<TEntity>> ListAsync(CancellationToken cancellationToken = new CancellationToken());

    Task<ICollection<TEntity>> SearchAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = new CancellationToken());

    Task<IPagedList<TEntity>> SearchAsync(IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken());

    Task<IPagedList<TEntity>> SearchAsync(ISpecification<TEntity> specification, IPageSearch pageSearch, CancellationToken cancellationToken = new CancellationToken());

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
