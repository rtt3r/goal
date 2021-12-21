using System.Collections;

namespace Vantage.Infra.Crosscutting.Collections
{
    public interface IPagedCollection : IEnumerable
    {
        int PageCount { get; }
        int TotalCount { get; }
    }
}
