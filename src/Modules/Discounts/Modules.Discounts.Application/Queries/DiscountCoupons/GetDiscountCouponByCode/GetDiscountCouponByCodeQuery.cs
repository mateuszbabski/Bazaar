using MediatR;
using Modules.Discounts.Application.Dtos;

namespace Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCouponByCode
{
    public class GetDiscountCouponByCodeQuery : IRequest<DiscountCouponDto>
    {
        public string DiscountCode { get; set; }
    }
}
