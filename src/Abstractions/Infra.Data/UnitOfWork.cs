using System;
using System.Threading;
using System.Threading.Tasks;
using Goal.Domain;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infra.Data;

public abstract class UnitOfWork : IUnitOfWork
{
    private readonly DbContext context;
    private bool disposed;

    protected UnitOfWork(DbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        this.context = context;
    }

    public int Commit()
        => context.SaveChanges();

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken);

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
