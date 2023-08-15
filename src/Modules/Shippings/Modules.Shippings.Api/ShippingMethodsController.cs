using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Shippings.Application.Commands.ShippingMethods.AddShippingMethod;
using Modules.Shippings.Application.Commands.ShippingMethods.ChangeShippingMethodAvailability;
using Modules.Shippings.Application.Commands.ShippingMethods.UpdateShippingMethodDetails;
using Modules.Shippings.Application.Dtos;
using Modules.Shippings.Application.Queries.ShippingMethods.GetShippingMethodById;
using Modules.Shippings.Application.Queries.ShippingMethods.GetShippingMethods;
using Shared.Application.Queries;
using Shared.Domain.ValueObjects;
using Swashbuckle.Swagger.Annotations;

namespace Modules.Shippings.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingMethodsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ShippingMethodsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("AddShippingMethod")]
        [SwaggerOperation("Add shipping method")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Guid>> AddShippingMethod(AddShippingMethodCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("ChangeShippingMethodAvailability")]
        [SwaggerOperation("Change shipping method availability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> ChangeShippingMethodAvailability(ChangeShippingMethodAvailabilityCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("UpdateShippingMethodDetails")]
        [SwaggerOperation("Update shipping method details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> UpdateShippingMethodDetails(UpdateShippingMethodDetailsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetShippingMethods")]
        [SwaggerOperation("Get shipping methods")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<ShippingMethodDto>>> GetAllShops([FromQuery] GetAllShippingMethodsQuery query)
        {
            var shippingMethods = await _mediator.Send(query);

            return Ok(shippingMethods);
        }

        [HttpGet("{id}", Name = "GetShippingMethodById")]
        [SwaggerOperation("Get shipping method by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShippingMethodDto>> GetShippingMethodById(Guid id)
        {
            var shippingMethod = await _mediator.Send(new GetShippingMethodByIdQuery()
            {
                Id = id
            });

            return Ok(shippingMethod);
        }
    }
}
