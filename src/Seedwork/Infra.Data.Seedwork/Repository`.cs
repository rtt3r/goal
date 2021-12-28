using System;
using Goal.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infra.Data
{
    public abstract class Repository<TEntity> : Repository<TEntity, Guid>, IRepository<TEntity>
        where TEntity : class
    {
        public Repository(DbContext context)
            : base(context)
        {
        }
    }
}
