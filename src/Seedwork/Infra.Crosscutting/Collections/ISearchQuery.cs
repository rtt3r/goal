using System.Collections.Generic;

namespace Goal.Seedwork.Infra.Crosscutting.Collections
{
    public interface ISearchQuery
    {
        int PageIndex { get; }

        int PageSize { get; }

        IEnumerable<SortQuery> Sort { get; }
    }
}
