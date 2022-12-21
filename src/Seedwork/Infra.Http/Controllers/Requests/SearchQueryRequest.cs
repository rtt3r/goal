using System.Collections;
using System.Collections.Generic;

namespace Goal.Seedwork.Infra.Http.Controllers.Requests
{
    public sealed class SearchQueryRequest
    {
        /// <summary>
        /// The required page index (starts at zero)
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// The number of page items
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The field name to sort
        /// </summary>
        public IEnumerable<SortQueryRequest> Sort { get; set; }
    }
}
