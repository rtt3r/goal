namespace Goal.Infra.Data.Query;

public abstract class QueryRepository<TEntity> : QueryRepository<TEntity, string>, IQueryRepository<TEntity, string>
    where TEntity : class
{
}
