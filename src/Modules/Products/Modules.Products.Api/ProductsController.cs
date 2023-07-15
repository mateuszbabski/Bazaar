using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Products.Application.Commands.AddProduct;
using Modules.Products.Application.Commands.ChangeProductAvailability;
using Modules.Products.Application.Commands.ChangeProductDetails;
using Modules.Products.Application.Commands.ChangeProductPrice;
using Modules.Products.Application.Commands.ChangeProductWeight;
using Modules.Products.Application.Dtos;
using Modules.Products.Application.Queries.GetAllProducts;
using Modules.Products.Application.Queries.GetProductById;
using Modules.Products.Application.Queries.GetProductsByCategory;
using Modules.Products.Application.Queries.GetProductsByName;
using Modules.Products.Application.Queries.GetProductsByPriceRange;
using Modules.Products.Application.Queries.GetProductsByShopId;
using Shared.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Products.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "shop")]
        [HttpPost("AddProduct")]
        [SwaggerOperation("Add product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Guid>> AddProduct(AddProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "shop")]
        [HttpPatch("ChangeProductAvailability")]
        [SwaggerOperation("Change product availability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> ChangeProductAvailability(ChangeProductAvailabilityCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "shop")]
        [HttpPatch("ChangeProductDetails")]
        [SwaggerOperation("Change product details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> ChangeProductDetails(ChangeProductDetailsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "shop")]
        [HttpPatch("ChangeProductPrice")]
        [SwaggerOperation("Change product price")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> ChangeProductPrice(ChangeProductPriceCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "shop")]
        [HttpPatch("ChangeProductWeight")]
        [SwaggerOperation("Change product weight")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> ChangeProductWeight(ChangeProductWeightCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetProducts")]
        [SwaggerOperation("Get products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<ProductDto>>> GetAllShops([FromQuery] GetAllProductsQuery query)
        {
            var shops = await _mediator.Send(query);

            return Ok(shops);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        [SwaggerOperation("Get product by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDetailsDto>> GetProductById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery()
            {
                Id = id
            });

            return Ok(product);
        }

        [HttpGet("GetProductByCategory")]
        [SwaggerOperation("Get product by Category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<ProductDto>>> GetProductsByCategory([FromQuery] GetProductsByCategoryQuery query)
        {
            var shop = await _mediator.Send(query);

            return Ok(shop);
        }

        [HttpGet("GetProductByName")]
        [SwaggerOperation("Get product by Name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<ProductDto>>> GetProductsByName([FromQuery] GetProductsByNameQuery query)
        {
            var shop = await _mediator.Send(query);

            return Ok(shop);
        }

        [HttpGet("GetProductByPriceRange")]
        [SwaggerOperation("Get product by PriceRange")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<ProductDto>>> GetProductsByPriceRange([FromQuery] GetProductsByPriceRangeQuery query)
        {
            var shop = await _mediator.Send(query);

            return Ok(shop);
        }

        [HttpGet("GetProductByShopId")]
        [SwaggerOperation("Get product by ShopId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<ProductDto>>> GetProductsByShopId([FromQuery] GetProductsByShopIdQuery query)
        {
            var shop = await _mediator.Send(query);

            return Ok(shop);
        }
    }
}
