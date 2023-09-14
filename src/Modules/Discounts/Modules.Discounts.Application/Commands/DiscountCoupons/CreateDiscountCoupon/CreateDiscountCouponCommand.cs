using MediatR;

namespace Modules.Discounts.Application.Commands.DiscountCoupons.CreateDiscountCoupon
{
    public class CreateDiscountCouponCommand : IRequest<Guid>
    {
        public Guid DiscountId { get; set; }
        public DateTimeOffset StartsAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset ExpirationDate { get; set; } = DateTimeOffset.Now.AddYears(1);
    }
}
