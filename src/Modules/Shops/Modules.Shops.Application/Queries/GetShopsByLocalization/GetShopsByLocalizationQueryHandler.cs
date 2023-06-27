using MediatR;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Domain.Repositories;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Modules.Shops.Application.Queries.GetShopsByLocalization
{
    public class GetShopsByLocalizationQueryHandler : IRequestHandler<GetShopsByLocalizationQuery, PagedList<ShopDto>>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopsByLocalizationQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<PagedList<ShopDto>> Handle(GetShopsByLocalizationQuery query, CancellationToken cancellationToken)
        {
            var shops = await _shopRepository.GetShopsByLocalization(query.Country, query.City)
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
