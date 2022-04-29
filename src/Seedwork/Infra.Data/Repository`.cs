using System;
using Goal.Seedwork.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Goal.Seedwork.Infra.Data
{
    public abstract class Repository<TEntity> : Repository<TEntity, Guid>, IRepository<TEntity>
        where TEntity : class
    {
        protected Repository(DbContext context)
            : base(context)
        {
        }
    }
}
