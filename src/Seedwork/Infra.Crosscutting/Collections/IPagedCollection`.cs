using System.Collections.Generic;

namespace Goal.Infra.Crosscutting.Collections
{
    public interface IPagedCollection<out T> : IPagedCollection, IEnumerable<T>
    {
    }
}
