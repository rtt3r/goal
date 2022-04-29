using System.Collections.Generic;

namespace Goal.Seedwork.Infra.Crosscutting.Collections
{
    public interface IPagedCollection<out T> : IPagedCollection, IEnumerable<T>
    {
    }
}
