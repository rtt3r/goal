using System;
using Goal.Seedwork.Domain.Aggregates;

namespace Goal.Seedwork.Infra.Data
{
    public abstract class Repository : IRepository
    {
        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
