using MediatR;
using Modules.Shippings.Application.Dtos;
using Modules.Shippings.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Shippings.Application.Queries.ShippingMethods.GetShippingMethodById
{
    public class GetShippingMethodByIdQueryHandler : IRequestHandler<GetShippingMethodByIdQuery, ShippingMethodDto>
    {
        private readonly IShippingMethodRepository _shippingMethodRepository;

        public GetShippingMethodByIdQueryHandler(IShippingMethodRepository shippingMethodRepository)
        {
            _shippingMethodRepository = shippingMethodRepository;
        }
        public async Task<ShippingMethodDto> Handle(GetShippingMethodByIdQuery query, CancellationToken cancellationToken)
        {
            var shippingMethod = await _shippingMethodRepository.GetShippingMethodById(query.Id)
                ?? throw new NotFoundException("Shipping method not found");

            var shippingMethodDto = ShippingMethodDto.CreateDtoFromObject(shippingMethod);

            return shippingMethodDto;
        }
    }
}
