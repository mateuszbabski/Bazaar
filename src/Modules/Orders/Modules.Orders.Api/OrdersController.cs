using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Application.Queries.GetOrderById;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Orders.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator _mediator)
        {
            this._mediator = _mediator;
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        [SwaggerOperation("Get order by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderById(Guid id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery()
            {
                Id = id
            });

            return Ok(order);
        }
    }
}
