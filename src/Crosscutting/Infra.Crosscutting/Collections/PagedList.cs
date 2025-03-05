using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Goal.Infra.Crosscutting.Collections;

[DebuggerStepThrough]
[DebuggerDisplay("PageCount = {PageCount}; TotalCount = {TotalCount}")]
public class PagedList<T> : IPagedList<T>
{
    private readonly IEnumerable<T> items;

    public PagedList(IEnumerable<T>? items)
        : this(items, items?.Count())
    {
    }

    public PagedList(IEnumerable<T>? items, long? totalCount)
    {
        this.items = items ?? [];

        TotalCount = totalCount ?? 0;
        PageLength = this.items.Count();
    }

    public long TotalCount { get; init; }

    public int PageLength { get; init; }

    public IEnumerator<T> GetEnumerator()
        => items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => items.GetEnumerator();
}
