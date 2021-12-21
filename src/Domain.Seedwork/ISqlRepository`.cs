namespace Vantage.Domain
{
    public interface ISqlRepository<TEntity> : ISqlRepository<TEntity, long>
        where TEntity : class
    {
    }
}
