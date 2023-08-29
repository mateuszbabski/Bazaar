using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Discounts.Application.Commands.Discounts.CreateDiscount;
using Modules.Discounts.Application.Commands.Discounts.DeleteDiscount;
using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Application.Queries.Discounts.GetDiscountById;
using Modules.Discounts.Application.Queries.Discounts.GetDiscounts;
using Modules.Discounts.Application.Queries.Discounts.GetDiscountsByType;
using Shared.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Discounts.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiscountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin, shop")]
        [HttpPost("CreateDiscount")]
        [SwaggerOperation("Create product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Guid>> CreateDiscount(CreateDiscountCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "admin, shop")]
        [HttpPost("RemoveDiscount")]
        [SwaggerOperation("Remove discount")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveDiscount(DeleteDiscountCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}", Name = "GetDiscountById")]
        [SwaggerOperation("Get discount by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DiscountDto>> GetDiscountById(Guid id)
        {
            var discount = await _mediator.Send(new GetDiscountByIdQuery()
            {
                Id = id
            });

            return Ok(discount);
        }

        [HttpGet("GetDiscountsByType")]
        [SwaggerOperation("Get discounts by Type")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<DiscountDto>>> GetDiscountsByType([FromQuery] GetDiscountsByTypeQuery query)
        {
            var discounts = await _mediator.Send(query);

            return Ok(discounts);
        }

        [Authorize(Roles = "admin, shop")]
        [HttpGet("GetDiscounts")]
        [SwaggerOperation("Get discounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<DiscountDto>>> GetAllShops([FromQuery] GetDiscountsQuery query)
        {
            var discounts = await _mediator.Send(query);

            return Ok(discounts);
        }
    }
}
