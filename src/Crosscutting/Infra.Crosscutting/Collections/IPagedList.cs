using System.Collections;

namespace Goal.Infra.Crosscutting.Collections;

public interface IPagedList : IEnumerable
{
    int PageLength { get; }
    long TotalCount { get; }
}
