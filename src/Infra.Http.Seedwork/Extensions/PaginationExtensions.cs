using Vantage.Infra.Crosscutting.Collections;
using Vantage.Infra.Http.Controllers.Requests;

namespace Vantage.Infra.Http.Extensions
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
