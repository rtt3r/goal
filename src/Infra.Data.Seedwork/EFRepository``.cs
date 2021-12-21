using Vantage.Domain;

namespace Vantage.Infra.Data
{
    public abstract class EFRepository<TEntity> : EFRepository<TEntity, long>, ISqlRepository<TEntity>
        where TEntity : class
    {
        protected EFRepository(IEFUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
