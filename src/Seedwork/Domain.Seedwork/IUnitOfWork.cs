using System;

namespace Goal.Domain.Seedwork
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
