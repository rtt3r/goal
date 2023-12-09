using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Goal.Seedwork.Infra.Crosscutting.Collections;

[DebuggerStepThrough]
[DebuggerDisplay("PageCount = {PageCount}; TotalCount = {TotalCount}")]
public class PagedList<T>(IEnumerable<T>? items, long totalCount) : IPagedList<T>
{
    private readonly IEnumerable<T> items = items ?? new List<T>();

    public long TotalCount { get; private set; } = totalCount;

    public int PageCount { get; private set; } = items?.Count() ?? 0;

    public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();
}
