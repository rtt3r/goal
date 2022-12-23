using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Seedwork.Infra.Http.Controllers.Requests
{
    public sealed class SearchQueryRequest
    {
        /// <summary>
        /// The required page index (starts at zero)
        /// </summary>
        public int PageIndex { get; set; } = 0;

        /// <summary>
        /// The number of page items
        /// </summary>
        public int PageSize { get; set; } = int.MaxValue;

        /// <summary>
        /// The field name to sort
        /// </summary>
        public string SortBy { get; private set; }

        /// <summary>
        /// The sort direction (Asc | Desc)
        /// </summary>
        public SortDirection? SortDirection { get; private set; }
    }
}
