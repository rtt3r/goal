using System;
using Goal.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;

namespace Goal.Infra.Data.Seedwork
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;
        private bool disposed;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public bool Commit() => context.SaveChanges() > 0;

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
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
