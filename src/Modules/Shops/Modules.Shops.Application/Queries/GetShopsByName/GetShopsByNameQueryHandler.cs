using MediatR;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Domain.Repositories;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Modules.Shops.Application.Queries.GetShopsByName
{
    public class GetShopsByNameQueryHandler : IRequestHandler<GetShopsByNameQuery, PagedList<ShopDto>>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopsByNameQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<PagedList<ShopDto>> Handle(GetShopsByNameQuery query, CancellationToken cancellationToken)
        {
            var shops = await _shopRepository.GetShopsByName(query.ShopName)
                ?? throw new NotFoundException("Shops not found");

            var pagedShops = shops.Skip((query.PageNumber - 1) * query.PageSize)
                                  .Take(query.PageSize)
                                  .ToList();

            var shopListDto = ShopDto.CreateDtoFromObject(pagedShops);

            var pagedShopList = new PagedList<ShopDto>(shopListDto,
                                                       shopListDto.Count(),
                                                       query.PageNumber,
                                                       query.PageSize);

            return pagedShopList;
        }
    }
}
