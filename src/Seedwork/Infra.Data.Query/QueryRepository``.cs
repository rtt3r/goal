using System;

namespace Goal.Seedwork.Infra.Data.Query;

public abstract class QueryRepository<TEntity> : QueryRepository<TEntity, Guid>, IQueryRepository<TEntity, Guid>
    where TEntity : class
{
}
