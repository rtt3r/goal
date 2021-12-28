using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Requests;
using Goal.Infra.Http.Controllers.Results;
using Goal.Infra.Http.Extensions;
using Goal.Demo.Application.DTO.People.Requests;
using Goal.Demo.Application.DTO.People.Responses;
using Goal.Demo.Application.People;

namespace Goal.Demo.Api.Controllers.V2
{
    /// <summary>
    /// Everything about People
    /// </summary>
    [ApiController]
    //[ApiVersion("2")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class PeopleController : ApiController
    {
        private readonly IPersonAppService personAppService;

        public PeopleController(IPersonAppService personAppService)
        {
            this.personAppService = personAppService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<PersonResponse>>> Get([FromQuery] PaginationRequest request)
            => Paged(await personAppService.FindPaginatedAsync(request.ToPagination()));

        [HttpGet("{id:Guid}", Name = nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonResponse>> GetById(string id)
        {
            PersonResponse person = await personAppService.GetPersonAsync(id);

            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonResponse>> Post([FromBody] AddPersonRequest request)
        {
            return (await personAppService.AddPerson(request))
                .Match<ActionResult>(
                    failure: ex => BadRequest(ex.Message),
                    success: result => CreatedAtRoute(
                        nameof(GetById),
                        new { id = result.PersonId },
                        result));
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonResponse>> Patch(string id, [FromBody] UpdatePersonRequest request)
        {
            return (await personAppService.UpdatePerson(id, request))
                .Match<ActionResult>(
                    failure: ex => ex.IsBusiness()
                        ? BadRequest(ex.Message)
                        : NotFound(ex.Message),
                    success: result => AcceptedAtRoute(
                        nameof(GetById),
                        new { id = result.PersonId },
                        result));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
        {
            return (await personAppService.DeletePerson(id))
                .Match<ActionResult>(
                    failure: ex => NotFound(ex.Message),
                    success: result => Accepted());
        }
    }
}
