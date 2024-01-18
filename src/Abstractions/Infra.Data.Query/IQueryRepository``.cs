using System;

namespace Goal.Infra.Data.Query;

public interface IQueryRepository<TEntity> : IQueryRepository<TEntity, Guid>
    where TEntity : class
{
}
