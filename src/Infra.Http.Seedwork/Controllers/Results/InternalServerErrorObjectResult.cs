using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vantage.Infra.Http.Controllers
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error) : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}
