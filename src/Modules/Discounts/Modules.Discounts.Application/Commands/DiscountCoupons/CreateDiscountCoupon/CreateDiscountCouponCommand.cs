using MediatR;

namespace Modules.Discounts.Application.Commands.DiscountCoupons.CreateDiscountCoupon
{
    public class CreateDiscountCouponCommand : IRequest<Guid>
    {
        public Guid DiscountId { get; set; }
        public DateTimeOffset StartsAt { get; private set; } = DateTimeOffset.Now;
        public DateTimeOffset ExpirationDate { get; private set; } = DateTimeOffset.Now.AddYears(1);
    }
}
