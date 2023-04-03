using System;
using System.Threading;
using System.Threading.Tasks;

namespace Goal.Seedwork.Domain;

public interface IUnitOfWork : IDisposable
{
    bool Save();
    Task<bool> SaveAsync(CancellationToken cancellationToken = default);
}
