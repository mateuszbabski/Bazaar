using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Shops.Application.Commands.SignInShop;
using Modules.Shops.Application.Commands.SignUpShop;
using Modules.Shops.Application.Commands.UpdateShopDetails;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Application.Queries.GetShopById;
using Modules.Shops.Application.Queries.GetShops;
using Modules.Shops.Application.Queries.GetShopsByLocalization;
using Modules.Shops.Application.Queries.GetShopsByName;
using Shared.Application.Auth;
using Shared.Application.Queries;
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

        [HttpPost("SignUp")]
        [SwaggerOperation("Sign up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticationResult>> SignUpAsync(SignUpShopCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("SignIn")]
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateShopDetails(UpdateShopDetailsCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}", Name = "GetShopById")]
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

        [HttpGet("GetShops")]
        [SwaggerOperation("Get shops")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<ShopDto>>> GetAllShops([FromQuery] GetShopsQuery query)
        {
            var shops = await _mediator.Send(query);

            return Ok(shops);
        }

        [HttpGet("GetShopsByName")]
        [SwaggerOperation("Get shops by Name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<ShopDto>>> GetShopsByName([FromQuery] GetShopsByNameQuery query)
        {
            var shop = await _mediator.Send(query);

            return Ok(shop);
        }

        [HttpGet("GetShopsByLocalization")]
        [SwaggerOperation("Get shops by Localization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<ShopDto>>> GetShopsByLocalization([FromQuery] GetShopsByLocalizationQuery query)
        {
            var shop = await _mediator.Send(query);

            return Ok(shop);
        }
    }
}
