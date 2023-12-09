using System.Collections;

namespace Goal.Seedwork.Infra.Crosscutting.Collections;

public interface IPagedList : IEnumerable
{
    int PageCount { get; }
    long TotalCount { get; }
}
