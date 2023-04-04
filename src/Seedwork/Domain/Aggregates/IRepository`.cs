using System;

namespace Goal.Seedwork.Domain.Aggregates;

public interface IRepository<TEntity> : IRepository<TEntity, Guid>
    where TEntity : class
{
}
