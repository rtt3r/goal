namespace Goal.Domain.Aggregates;

public interface IRepository<TEntity> : IRepository<TEntity, string>
    where TEntity : class
{
}
