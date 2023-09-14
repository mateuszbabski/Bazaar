using MediatR;

namespace Modules.Discounts.Application.Commands.DiscountCoupons.DisableDiscountCoupon
{
    public class DisableDiscountCouponCommand : IRequest<Unit>
    {
        public Guid DiscountCouponId { get; set; }
    }
}
