using System.Collections.Generic;

namespace Vantage.Infra.Crosscutting.Collections
{
    public interface IPagedCollection<out T> : IPagedCollection, IEnumerable<T>
    {
    }
}
