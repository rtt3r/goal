using System;

namespace Goal.Domain.Aggregates
{
    public interface IRepository<TEntity> : IRepository<TEntity, Guid>
        where TEntity : class
    {
    }
}
