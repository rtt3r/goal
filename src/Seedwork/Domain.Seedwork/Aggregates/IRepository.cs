using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Specifications;

namespace Goal.Domain.Aggregates
{
    public interface IRepository<TEntity, TKey> : IDisposable
        where TEntity : class
    {
        TEntity Find(TKey id);

        ICollection<TEntity> Find();

        ICollection<TEntity> Find(ISpecification<TEntity> specification);

        IPagedCollection<TEntity> Find(IPagination pagination);

        IPagedCollection<TEntity> Find(ISpecification<TEntity> specification, IPagination pagination);

        Task<TEntity> FindAsync(TKey id);

        Task<ICollection<TEntity>> FindAsync();

        Task<ICollection<TEntity>> FindAsync(ISpecification<TEntity> specification);

        Task<IPagedCollection<TEntity>> FindAsync(IPagination pagination);

        Task<IPagedCollection<TEntity>> FindAsync(ISpecification<TEntity> specification, IPagination pagination);

        bool Any();

        bool Any(ISpecification<TEntity> specification);

        Task<bool> AnyAsync();

        Task<bool> AnyAsync(ISpecification<TEntity> specification);

        void Add(TEntity entity);

        void Add(IEnumerable<TEntity> entities);

        Task AddAsync(TEntity entity);

        Task AddAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void Remove(IEnumerable<TEntity> entities);

        void Remove(ISpecification<TEntity> specification);
    }
}
