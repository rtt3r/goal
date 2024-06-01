using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Goal.Domain.Aggregates;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Goal.Infra.Crosscutting.Specifications;
using Goal.Infra.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infra.Data;

public abstract class Repository<TEntity, TKey> : Repository, IRepository<TEntity, TKey>
    where TEntity : class
{
    private bool disposed;

    protected DbContext Context { get; private set; }

    protected Repository(DbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        Context = context;
    }

    public virtual TEntity? Load(TKey key)
        => Context.Set<TEntity>().Find(key);

    public virtual ICollection<TEntity> Query()
        => Context.Set<TEntity>().ToList();

    public virtual ICollection<TEntity> Query(ISpecification<TEntity> specification)
    {
        return FindSpecific(specification)
            .ToList();
    }

    public virtual IPagedList<TEntity> Query(ISpecification<TEntity> specification, IPageSearch pageSearch)
    {
        ArgumentNullException.ThrowIfNull(pageSearch);

        return FindSpecific(specification)
            .ToPagedList(pageSearch);
    }

    public virtual IPagedList<TEntity> Query(IPageSearch pageSearch)
        => Query(new TrueSpecification<TEntity>(), pageSearch);

    public virtual async Task<TEntity?> LoadAsync(TKey key, CancellationToken cancellationToken = new CancellationToken())
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await Context.Set<TEntity>().FindAsync([key], cancellationToken);
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

    public virtual async Task<IPagedList<TEntity>> QueryAsync(
        IPageSearch pageSearch,
        CancellationToken cancellationToken = new CancellationToken())
        => await QueryAsync(new TrueSpecification<TEntity>(), pageSearch, cancellationToken);

    public virtual async Task<IPagedList<TEntity>> QueryAsync(
        ISpecification<TEntity> specification,
        IPageSearch pageSearch,
        CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(pageSearch);

        return await FindSpecific(specification)
            .ToPagedListAsync(pageSearch, cancellationToken);
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
        ArgumentNullException.ThrowIfNull(entity);
        Context.Set<TEntity>().Add(entity);
    }

    public virtual void Add(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        Context.Set<TEntity>().AddRange(entities);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(entity);
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = new CancellationToken())
    {
        ArgumentNullException.ThrowIfNull(entities);
        await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Context.Set<TEntity>().Update(entity);
    }

    public virtual void Update(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        Context.Set<TEntity>().UpdateRange(entities);
    }

    public virtual void Remove(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        Context.Set<TEntity>().Remove(entity);
    }

    public virtual void Remove(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        Context.Set<TEntity>().RemoveRange(entities);
    }

    public virtual void Remove(ISpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        var entities = Context
            .Set<TEntity>()
            .Where(specification.SatisfiedBy())
            .ToList();

        Context.Set<TEntity>().RemoveRange(entities);
    }

    private IQueryable<TEntity> FindSpecific(ISpecification<TEntity> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);
        return Context.Set<TEntity>().Where(specification.SatisfiedBy());
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Context.Dispose();
                Context = null!;
            }

            disposed = true;
        }
    }
}
