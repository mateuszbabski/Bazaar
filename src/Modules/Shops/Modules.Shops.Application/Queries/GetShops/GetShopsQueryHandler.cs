using MediatR;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Shops.Application.Queries.GetShops
{
    public class GetShopsQueryHandler : IRequestHandler<GetShopsQuery, IEnumerable<ShopDto>>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopsQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<IEnumerable<ShopDto>> Handle(GetShopsQuery query, CancellationToken cancellationToken)
        {
            var shops = await _shopRepository.GetAllShops()
                ?? throw new NotFoundException("Shops not found");

            var shopListDto = ShopDto.CreateDtoFromObject(shops);

            return shopListDto;
        }
    }
}
