using System.Collections;

namespace Goal.Seedwork.Infra.Crosscutting.Collections;

public interface IPagedList : IEnumerable
{
    int PageLength { get; }
    long TotalCount { get; }
}
