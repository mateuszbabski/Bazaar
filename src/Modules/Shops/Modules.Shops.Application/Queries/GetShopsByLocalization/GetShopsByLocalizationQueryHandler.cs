using MediatR;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Shops.Application.Queries.GetShopsByLocalization
{
    public class GetShopsByLocalizationQueryHandler : IRequestHandler<GetShopsByLocalizationQuery, IEnumerable<ShopDto>>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopsByLocalizationQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<IEnumerable<ShopDto>> Handle(GetShopsByLocalizationQuery request, CancellationToken cancellationToken)
        {
            var shops = await _shopRepository.GetShopsByLocalization(request.Country, request.City)
                ?? throw new NotFoundException("Shops not found");

            var shopListDto = ShopDto.CreateDtoFromObject(shops);

            return shopListDto;
        }
    }
}
