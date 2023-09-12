using MediatR;
using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Discounts.Application.Queries.Discounts.GetDiscountById
{
    public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, DiscountDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public GetDiscountByIdQueryHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<DiscountDto> Handle(GetDiscountByIdQuery query, CancellationToken cancellationToken)
        { // role conditions
            var discount = await _discountRepository.GetDiscountById(query.Id)
                ?? throw new NotFoundException("Discount not found");

            var discountDto = DiscountDto.CreateDtoFromObject(discount);

            return discountDto;
        }
    }
}
