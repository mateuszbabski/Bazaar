using MediatR;
using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCouponById
{
    public class GetDiscountCouponByIdQueryHandler : IRequestHandler<GetDiscountCouponByIdQuery, DiscountCouponDto>
    {
        private readonly IDiscountCouponRepository _discountCouponRepository;

        public GetDiscountCouponByIdQueryHandler(IDiscountCouponRepository discountCouponRepository)
        {
            _discountCouponRepository = discountCouponRepository;
        }

        public async Task<DiscountCouponDto> Handle(GetDiscountCouponByIdQuery query, CancellationToken cancellationToken)
        {
            var discountCoupon = await _discountCouponRepository.GetDiscountCouponById(query.Id)
                ?? throw new NotFoundException("Discount Coupon doesnt exist");

            var discountCouponDto = DiscountCouponDto.CreateDtoFromObject(discountCoupon);

            return discountCouponDto;
        }
    }
}
