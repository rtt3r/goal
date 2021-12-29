using System;
using System.Threading.Tasks;
using Goal.Application.Seedwork.Extensions;
using Goal.Application.Seedwork.Handlers;
using Goal.Demo2.Api.Application.Commands.Customers;
using Goal.Demo2.Api.Application.Dtos.Customers;
using Goal.Demo2.Domain.Aggregates.Customers;
using Goal.Infra.Crosscutting.Adapters;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Http.Seedwork.Controllers;
using Goal.Infra.Http.Seedwork.Controllers.Requests;
using Goal.Infra.Http.Seedwork.Controllers.Results;
using Goal.Infra.Http.Seedwork.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Goal.Demo2.Api.Controllers
{
    /// <summary>
    /// Everything about Customers
    /// </summary>
    [ApiController]
    //[ApiVersion("2")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IBusHandler busHandler;
        private readonly INotificationHandler notificationHandler;
        private readonly ITypeAdapter typeAdapter;

        public CustomerController(
            ICustomerRepository customerRepository,
            IBusHandler busHandler,
            INotificationHandler notificationHandler,
            ITypeAdapter typeAdapter)
        {
            this.customerRepository = customerRepository;
            this.busHandler = busHandler;
            this.notificationHandler = notificationHandler;
            this.typeAdapter = typeAdapter;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<CustomerDto>>> Get([FromQuery] PaginationRequest request)
        {
            IPagedCollection<Customer> customers = await customerRepository.FindAsync(request.ToPagination());
            return Paged(typeAdapter.ProjectAsPagedCollection<CustomerDto>(customers));
        }

        [HttpGet("{id:Guid}", Name = nameof(GetById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> GetById(Guid id)
        {
            Customer customer = await customerRepository.FindAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            return Ok(typeAdapter.ProjectAs<CustomerDto>(customer));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> Post([FromBody] RegisterNewCustomerCommand command)
        {
            Guid id = await busHandler.SendCommand<RegisterNewCustomerCommand, Guid>(command);

            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            return CreatedAtRoute(
                nameof(GetById),
                new { id },
                null);
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> Patch(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            bool success = await busHandler.SendCommand(command);

            if (!success)
            {
                return BadRequest();
            }

            return AcceptedAtAction(
                nameof(GetById),
                new { id },
                null);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            bool success = await busHandler.SendCommand(new RemoveCustomerCommand(id));

            if (success)
            {
                return NotFound();
            }

            return Accepted();
        }
    }
}
