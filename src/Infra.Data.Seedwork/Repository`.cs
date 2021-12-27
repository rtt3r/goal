using Microsoft.EntityFrameworkCore;
using Goal.Domain;

namespace Goal.Infra.Data
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
