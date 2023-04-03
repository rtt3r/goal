using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Http.Controllers.Requests;

namespace Goal.Seedwork.Infra.Http.Extensions;

public static class PaginationExtensions
{
    public static PageSearch ToPageSearch(this PageSearchRequest request)
    {
        return request is null
            ? new PageSearch()
            : new PageSearch(
            request.PageIndex,
            request.PageSize,
            request.SortBy,
            request.SortDirection.GetValueOrDefault(SortDirection.Asc));
    }
}
