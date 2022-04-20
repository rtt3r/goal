using System;
using System.Threading;
using System.Threading.Tasks;

namespace Goal.Domain.Seedwork
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    }
}
