using System;

namespace Goal.Infra.Data.Query;

public abstract class QueryRepository : IQueryRepository
{
    protected abstract void Dispose(bool disposing);

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
