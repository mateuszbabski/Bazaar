using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Shops.Application.Commands.SignInShop;
using Modules.Shops.Application.Commands.SignUpShop;
using Modules.Shops.Application.Commands.UpdateShopDetails;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Application.Queries.GetShopById;
using Shared.Application.Auth;
using Shared.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Shops.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sign-up")]
        [SwaggerOperation("Sign up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticationResult>> SignUpAsync(SignUpShopCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("sign-in")]
        [SwaggerOperation("Sign in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticationResult>> SignInAsync(SignInShopCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "shop")]
        [HttpPatch("UpdateShopDetails")]
        [SwaggerOperation("Update shop details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateShopDetails(UpdateShopDetailsCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("GetShopById")]
        [SwaggerOperation("Get shop by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShopDetailsDto>> GetShopById(Guid id)
        {
            var shop = await _mediator.Send(new GetShopByIdQuery()
            {
                Id = id
            });

            return Ok(shop);
        }
    }
}
