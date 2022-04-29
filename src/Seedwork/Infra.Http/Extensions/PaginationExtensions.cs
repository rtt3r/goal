using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Http.Controllers.Requests;

namespace Goal.Seedwork.Infra.Http.Extensions
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
