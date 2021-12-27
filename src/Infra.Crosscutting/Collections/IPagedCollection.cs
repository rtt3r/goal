using System.Collections;

namespace Goal.Infra.Crosscutting.Collections
{
    public interface IPagedCollection : IEnumerable
    {
        int PageCount { get; }
        int TotalCount { get; }
    }
}
