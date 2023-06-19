using MediatR;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Shops.Application.Queries.GetShopById
{
    public class GetShopByIdQueryHandler : IRequestHandler<GetShopByIdQuery, ShopDetailsDto>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopByIdQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<ShopDetailsDto> Handle(GetShopByIdQuery query, CancellationToken cancellationToken)
        {
            var shop = await _shopRepository.GetShopById(query.Id)
                ?? throw new NotFoundException("Shop not found.");

            var shopDto = ShopDetailsDto.CreateDtoFromObject(shop);

            return shopDto;
        }
    }
}
