using MediatR;
using Modules.Discounts.Application.Dtos;

namespace Modules.Discounts.Application.Queries.Discounts.GetDiscountByCouponCode
{
    public class GetDiscountByCouponCodeQuery : IRequest<DiscountDto>
    {
        public string CouponCode { get; set; }
    }
}
