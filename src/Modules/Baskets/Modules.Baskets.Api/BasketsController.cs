﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Baskets.Application.Commands.ChangeBasketCurrency;
using Modules.Baskets.Application.Commands.ChangeProductQuantity;
using Modules.Baskets.Application.Commands.CheckoutBasket;
using Modules.Baskets.Application.Commands.DeleteBasket;
using Modules.Baskets.Application.Commands.RemoveProductFromBasket;
using Modules.Baskets.Application.Dtos;
using Modules.Baskets.Application.Queries.GetBasketByCustomerId;
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
        [HttpPost("CheckoutBasket")]
        [SwaggerOperation("Checkout basket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> CheckoutBasket(CheckoutBasketCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Roles = "customer")]
        [HttpPost("ChangeBasketCurrency")]
        [SwaggerOperation("Change basket currency")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> ChangeBasketCurrency(ChangeBasketCurrencyCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [Authorize(Roles = "customer")]
        [HttpPost("ChangeProductQuantity")]
        [SwaggerOperation("Change product quantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> ChangeProductQuantity(ChangeProductQuantityCommand command)
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
        public async Task<ActionResult<Unit>> DeleteBasket(DeleteBasketCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = "customer")]
        [HttpPost("RemoveProductFromBasket")]
        [SwaggerOperation("Remove product from basket")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Unit>> RemoveProductFromBasket(RemoveProductFromBasketCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = "customer")]
        [HttpGet("GetBasketByCustomerId")]
        [SwaggerOperation("Get basket by customer Id")]
        public async Task<ActionResult<BasketDto>> GetBasketForCustomer()
        {
            var basket = await _mediator.Send(new GetBasketByCustomerIdQuery());

            return Ok(basket);
        }
    }
}
