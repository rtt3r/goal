using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Http.Controllers.Requests;

namespace Goal.Seedwork.Infra.Http.Extensions
{
    public static class PaginationExtensions
    {
        public static PageSearch ToSearchQuery(this SearchQueryRequest request)
        {
            if (request is null)
            {
                return new PageSearch();
            }

            return new PageSearch(
                request.PageIndex,
                request.PageSize,
                request.SortBy,
                request.SortDirection.GetValueOrDefault(SortDirection.Asc));
        }
    }
}
