using System;
using Goal.Domain.Seedwork.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infra.Data.Seedwork
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
