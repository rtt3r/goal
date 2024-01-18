namespace Goal.Domain.Abstractions.Aggregates;

public interface IRepository<TEntity> : IRepository<TEntity, string>
    where TEntity : class
{
}
