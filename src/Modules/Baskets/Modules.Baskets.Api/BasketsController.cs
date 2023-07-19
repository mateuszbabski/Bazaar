using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Baskets.Application.Commands.ChangeBasketCurrency;
using Modules.Baskets.Application.Commands.DeleteBasket;
using Shared.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Baskets.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "customer")]
        [HttpPost("ChangeBasketCurrency")]
        [SwaggerOperation("Change basket currency")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> ChangeBasketCurrency(ChangeBasketCurrencyCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [Authorize(Roles = "customer")]
        [HttpPost("DeleteBasket")]
        [SwaggerOperation("Delete basket")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> DeleteBasket(DeleteBasketCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
