using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Http.Seedwork.Controllers.Requests;

namespace Goal.Infra.Http.Seedwork.Extensions
{
    public static class PaginationExtensions
    {
        public static Pagination ToPagination(this PaginationRequest request)
        {
            if (request is null)
            {
                return new Pagination();
            }

            return new Pagination(
                request.PageIndex,
                request.PageSize,
                request.OrderByName,
                request.Ascending);
        }
    }
}
