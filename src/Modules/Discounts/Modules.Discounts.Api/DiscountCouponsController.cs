using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Discounts.Application.Commands.DiscountCoupons.CreateDiscountCoupon;
using Modules.Discounts.Application.Commands.DiscountCoupons.DisableDiscountCoupon;
using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCouponByCode;
using Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCouponById;
using Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCoupons;
using Modules.Discounts.Domain.ValueObjects;
using Shared.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Discounts.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountCouponsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiscountCouponsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin, shop")]
        [HttpPost("CreateDiscountCoupon")]
        [SwaggerOperation("Create discount coupon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Guid>> CreateDiscountCoupon(CreateDiscountCouponCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "admin, shop")]
        [HttpPost("DisableDiscountCoupon")]
        [SwaggerOperation("Disable discount coupon")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DisableDiscountCoupon(DisableDiscountCouponCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "admin, shop, customer")]
        [HttpGet("{id}", Name = "GetDiscountCouponById")]
        [SwaggerOperation("Get discount coupon by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DiscountCouponDto>> GetDiscountCouponById(Guid id)
        {
            var discountCoupon = await _mediator.Send(new GetDiscountCouponByIdQuery()
            {
                Id = id
            });

            return Ok(discountCoupon);
        }

        [Authorize(Roles = "admin, shop, customer")]
        [HttpGet("GetDiscountCouponByCode")]
        [SwaggerOperation("Get discount coupon by code")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<DiscountCouponDto>>> GetDiscountCouponByCode(string discountCode)
        {
            var discountCoupons = await _mediator.Send(new GetDiscountCouponByCodeQuery()
            {
                DiscountCode = discountCode
            });

            return Ok(discountCoupons);
        }

        [Authorize(Roles = "admin, shop, customer")]
        [HttpGet("GetDiscountCoupons")]
        [SwaggerOperation("Get discount coupons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagedList<DiscountCouponDto>>> GetAllDiscountCoupons([FromQuery] GetDiscountCouponsQuery query)
        {
            var discountCoupons = await _mediator.Send(query);

            return Ok(discountCoupons);
        }
    }
}
