using System;

namespace Goal.Infra.Data.Query.Seedwork
{
    public abstract class QueryRepository<TEntity> : QueryRepository<TEntity, Guid>, IQueryRepository<TEntity, Guid>
        where TEntity : class
    {
    }
}
