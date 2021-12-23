using Microsoft.EntityFrameworkCore;
using Vantage.Domain;

namespace Vantage.Infra.Data
{
    public abstract class Repository<TEntity> : Repository<TEntity, long>, IRepository<TEntity>
        where TEntity : class
    {
        public Repository(DbContext context)
            : base(context)
        {
        }
    }
}
