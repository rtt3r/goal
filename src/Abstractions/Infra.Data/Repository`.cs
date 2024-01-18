using Goal.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infra.Data;

public abstract class Repository<TEntity>(DbContext context) : Repository<TEntity, string>(context), IRepository<TEntity>
    where TEntity : class
{
}
