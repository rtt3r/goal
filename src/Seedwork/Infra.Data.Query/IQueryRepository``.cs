using System;

namespace Goal.Seedwork.Infra.Data.Query
{
    public interface IQueryRepository<TEntity> : IQueryRepository<TEntity, Guid>
        where TEntity : class
    {
    }
}
