using MediatR;
using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCouponByCode
{
    public class GetDiscountCouponByCodeQueryHandler : IRequestHandler<GetDiscountCouponByCodeQuery, DiscountCouponDto>
    {
        private readonly IDiscountCouponRepository _discountCouponRepository;

        public GetDiscountCouponByCodeQueryHandler(IDiscountCouponRepository discountCouponRepository)
        {
            _discountCouponRepository = discountCouponRepository;
        }

        public async Task<DiscountCouponDto> Handle(GetDiscountCouponByCodeQuery query, CancellationToken cancellationToken)
        {
            var discountCoupon = await _discountCouponRepository.GetDiscountCouponByCouponCode(query.DiscountCode)
                ?? throw new NotFoundException("Discount Coupon not found");

            var discountCouponDto = DiscountCouponDto.CreateDtoFromObject(discountCoupon);

            return discountCouponDto;
        }
    }
}
