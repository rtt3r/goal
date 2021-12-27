using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goal.Domain;
using Goal.Infra.Crosscutting;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Goal.Infra.Crosscutting.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infra.Data
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        private bool disposed;

        public DbContext Context { get; }

        public Repository(DbContext context)
        {
            Ensure.Argument.NotNull(context, nameof(context));
            Context = context;
        }

        public virtual TEntity Find(TKey id) => Context.Set<TEntity>().Find(id);

        public virtual ICollection<TEntity> Find() => Context.Set<TEntity>().ToList();

        public virtual ICollection<TEntity> Find(ISpecification<TEntity> specification)
        {
            return FindSpecific(specification)
                .ToList();
        }

        public virtual IPagedCollection<TEntity> Find(ISpecification<TEntity> specification, IPagination pagination)
        {
            Ensure.Argument.NotNull(pagination, nameof(pagination));

            return FindSpecific(specification)
                .PaginateList(pagination);
        }

        public virtual IPagedCollection<TEntity> Find(IPagination pagination) => Find(new TrueSpecification<TEntity>(), pagination);

        public virtual async Task<TEntity> FindAsync(TKey id) => await Context.Set<TEntity>().FindAsync(id);

        public virtual async Task<ICollection<TEntity>> FindAsync() => await Context.Set<TEntity>().ToListAsync();

        public virtual async Task<ICollection<TEntity>> FindAsync(ISpecification<TEntity> specification)
        {
            return await FindSpecific(specification)
                .ToListAsync();
        }

        public virtual async Task<IPagedCollection<TEntity>> FindAsync(IPagination pagination) => await FindAsync(new TrueSpecification<TEntity>(), pagination);

        public virtual async Task<IPagedCollection<TEntity>> FindAsync(ISpecification<TEntity> specification, IPagination pagination)
        {
            Ensure.Argument.NotNull(pagination, nameof(pagination));

            return await FindSpecific(specification)
                .PaginateListAsync(pagination);
        }

        public virtual bool Any()
        {
            return Context
                .Set<TEntity>()
                .AsNoTracking()
                .Any();
        }

        public virtual bool Any(ISpecification<TEntity> specification) => FindSpecific(specification).Any();

        public virtual async Task<bool> AnyAsync()
        {
            return await Context
                .Set<TEntity>()
                .AsNoTracking()
                .AnyAsync();
        }

        public virtual async Task<bool> AnyAsync(ISpecification<TEntity> specification) => await FindSpecific(specification).AnyAsync();

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

        public virtual async Task AddAsync(TEntity entity)
        {
            Ensure.Argument.NotNull(entity, nameof(entity));
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task AddAsync(IEnumerable<TEntity> entities)
        {
            Ensure.Argument.NotNull(entities, nameof(entities));
            await Context.Set<TEntity>().AddRangeAsync(entities);
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }

                disposed = true;
            }
        }

        //// TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        //~Repository()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //    Dispose(disposing: false);
        //}

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
