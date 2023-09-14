using MediatR;
using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Discounts.Application.Queries.Discounts.GetDiscountByCouponCode
{
    public class GetDiscountByCouponCodeQueryHandler : IRequestHandler<GetDiscountByCouponCodeQuery, DiscountDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public GetDiscountByCouponCodeQueryHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<DiscountDto> Handle(GetDiscountByCouponCodeQuery query, CancellationToken cancellationToken)
        {
            var discount = await _discountRepository.GetDiscountByCouponCode(query.CouponCode)
                ?? throw new NotFoundException("Discount not found.");

            var discountDto = DiscountDto.CreateDtoFromObject(discount);

            return discountDto;            
        }
    }
}
