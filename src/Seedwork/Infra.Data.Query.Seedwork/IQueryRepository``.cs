using System;

namespace Goal.Infra.Data.Query.Seedwork
{
    public interface IQueryRepository<TEntity> : IQueryRepository<TEntity, Guid>
        where TEntity : class
    {
    }
}
