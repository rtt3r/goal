using System;

namespace Goal.Infra.Data.Query;

public interface IQueryRepository<TEntity> : IQueryRepository<TEntity, string>
    where TEntity : class
{
}
