using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Domain.Aggregates;
using Goal.Seedwork.Infra.Crosscutting;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Crosscutting.Extensions;
using Goal.Seedwork.Infra.Crosscutting.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Goal.Seedwork.Infra.Data
{
    public abstract class Repository<TEntity, TKey> : Repository, IRepository<TEntity, TKey>
        where TEntity : class
    {
        private bool disposed;

        public DbContext Context { get; private set; }

        protected Repository(DbContext context)
        {
            Ensure.Argument.NotNull(context, nameof(context));
            Context = context;
        }

        public virtual TEntity Load(TKey id)
            => Context.Set<TEntity>().Find(id);

        public virtual ICollection<TEntity> Query()
            => Context.Set<TEntity>().ToList();

        public virtual ICollection<TEntity> Query(ISpecification<TEntity> specification)
        {
            return FindSpecific(specification)
                .ToList();
        }

        public virtual IPagedCollection<TEntity> Query(ISpecification<TEntity> specification, IPagination pagination)
        {
            Ensure.Argument.NotNull(pagination, nameof(pagination));

            return FindSpecific(specification)
                .PaginateList(pagination);
        }

        public virtual IPagedCollection<TEntity> Query(IPagination pagination)
            => Query(new TrueSpecification<TEntity>(), pagination);

        public virtual async Task<TEntity> LoadAsync(TKey id, CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<ICollection<TEntity>> QueryAsync(CancellationToken cancellationToken = new CancellationToken())
            => await Context.Set<TEntity>().ToListAsync(cancellationToken: cancellationToken);

        public virtual async Task<ICollection<TEntity>> QueryAsync(
            ISpecification<TEntity> specification,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return await FindSpecific(specification)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<IPagedCollection<TEntity>> QueryAsync(
            IPagination pagination,
            CancellationToken cancellationToken = new CancellationToken())
            => await QueryAsync(new TrueSpecification<TEntity>(), pagination, cancellationToken);

        public virtual async Task<IPagedCollection<TEntity>> QueryAsync(
            ISpecification<TEntity> specification,
            IPagination pagination,
            CancellationToken cancellationToken = new CancellationToken())
        {
            Ensure.Argument.NotNull(pagination, nameof(pagination));

            return await FindSpecific(specification)
                .PaginateListAsync(pagination, cancellationToken);
        }

        public virtual bool Any()
        {
            return Context
                .Set<TEntity>()
                .AsNoTracking()
                .Any();
        }

        public virtual bool Any(ISpecification<TEntity> specification)
            => FindSpecific(specification).Any();

        public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await Context
                .Set<TEntity>()
                .AsNoTracking()
                .AnyAsync(cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = new CancellationToken())
            => await FindSpecific(specification).AnyAsync(cancellationToken);

        public virtual void Add(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            Ensure.Argument.NotNull(entities, nameof(entities));
            Context.Set<TEntity>().AddRange(entities);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            Ensure.Argument.NotNull(entity, nameof(entity));
            await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public virtual async Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = new CancellationToken())
        {
            Ensure.Argument.NotNull(entities, nameof(entities));
            await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));
            Context.Set<TEntity>().Update(entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            Ensure.Argument.NotNull(entities, nameof(entities));
            Context.Set<TEntity>().UpdateRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void Remove(IEnumerable<TEntity> entities)
        {
            Ensure.Argument.NotNull(entities, nameof(entities));
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual void Remove(ISpecification<TEntity> specification)
        {
            Ensure.Argument.NotNull(specification, nameof(specification));

            var entities = Context
                .Set<TEntity>()
                .Where(specification.SatisfiedBy())
                .ToList();

            Context.Set<TEntity>().RemoveRange(entities);
        }

        private IQueryable<TEntity> FindSpecific(ISpecification<TEntity> specification)
        {
            Ensure.Argument.NotNull(specification, nameof(specification));
            return Context.Set<TEntity>().Where(specification.SatisfiedBy());
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                    Context = null;
                }

                disposed = true;
            }
        }
    }
}
