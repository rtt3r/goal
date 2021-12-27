namespace Goal.Domain.Aggregates
{
    public interface IRepository<TEntity> : IRepository<TEntity, long>
        where TEntity : class
    {
    }
}
