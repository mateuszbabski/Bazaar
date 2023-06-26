using MediatR;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Shops.Application.Queries.GetShopsByName
{
    public class GetShopsByNameQueryHandler : IRequestHandler<GetShopsByNameQuery, IEnumerable<ShopDto>>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopsByNameQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<IEnumerable<ShopDto>> Handle(GetShopsByNameQuery request, CancellationToken cancellationToken)
        {
            var shops = await _shopRepository.GetShopsByName(request.ShopName)
                ?? throw new NotFoundException("Shops not found");

            var shopListDto = ShopDto.CreateDtoFromObject(shops);

            return shopListDto;
        }
    }
}
