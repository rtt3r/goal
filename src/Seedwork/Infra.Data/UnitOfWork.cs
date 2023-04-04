using System;
using System.Threading;
using System.Threading.Tasks;
using Goal.Seedwork.Domain;
using Goal.Seedwork.Infra.Crosscutting;
using Microsoft.EntityFrameworkCore;

namespace Goal.Seedwork.Infra.Data;

public abstract class UnitOfWork : IUnitOfWork
{
    private readonly DbContext context;
    private bool disposed;

    protected UnitOfWork(DbContext context)
    {
        Ensure.Argument.IsNotNull(context, nameof(context));
        this.context = context;
    }

    public bool Save()
        => context.SaveChanges() > 0;

    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken) > 0;

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
