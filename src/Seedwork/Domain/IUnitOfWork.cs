using System;
using System.Threading;
using System.Threading.Tasks;

namespace Goal.Seedwork.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    }
}
