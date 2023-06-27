using MediatR;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Domain.Repositories;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Modules.Shops.Application.Queries.GetShops
{
    public class GetShopsQueryHandler : IRequestHandler<GetShopsQuery, PagedList<ShopDto>>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopsQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<PagedList<ShopDto>> Handle(GetShopsQuery query, CancellationToken cancellationToken)
        {
            var shops = await _shopRepository.GetAllShops()
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
