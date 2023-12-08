using System.Collections;

namespace Goal.Seedwork.Infra.Crosscutting.Collections;

public interface IPagedCollection : IEnumerable
{
    int PageCount { get; }
    long TotalCount { get; }
}
