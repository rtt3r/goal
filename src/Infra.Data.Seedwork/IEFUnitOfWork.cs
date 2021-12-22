using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vantage.Domain;

namespace Vantage.Infra.Data
{
    public interface IEFUnitOfWork : IUnitOfWork
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}
