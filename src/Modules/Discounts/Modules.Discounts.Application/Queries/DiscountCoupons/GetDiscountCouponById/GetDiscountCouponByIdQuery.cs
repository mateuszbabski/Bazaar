using MediatR;
using Modules.Discounts.Application.Dtos;

namespace Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCouponById
{
    public class GetDiscountCouponByIdQuery : IRequest<DiscountCouponDto>
    {
        public Guid Id { get; set; }
    }
}
