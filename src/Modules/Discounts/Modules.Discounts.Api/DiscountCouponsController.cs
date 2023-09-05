using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Discounts.Application.Commands.DiscountCoupons.CreateDiscountCoupon;
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
    }
}
