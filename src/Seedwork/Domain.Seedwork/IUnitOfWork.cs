using System;

namespace Goal.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
