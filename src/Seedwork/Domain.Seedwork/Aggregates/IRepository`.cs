using System;

namespace Goal.Domain.Seedwork.Aggregates
{
    public interface IRepository<TEntity> : IRepository<TEntity, Guid>
        where TEntity : class
    {
    }
}
