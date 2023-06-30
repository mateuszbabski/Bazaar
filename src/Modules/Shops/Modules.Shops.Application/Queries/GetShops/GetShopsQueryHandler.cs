using MediatR;
using Modules.Shops.Application.Dtos;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.Repositories;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Modules.Shops.Application.Queries.GetShops
{
    public class GetShopsQueryHandler : IRequestHandler<GetShopsQuery, PagedList<ShopDto>>
    {
        private readonly IShopRepository _shopRepository;
        private readonly IQueryProcessor<Shop> _queryProcessor;

        public GetShopsQueryHandler(IShopRepository shopRepository, IQueryProcessor<Shop> queryProcessor)
        {
            _shopRepository = shopRepository;
            _queryProcessor = queryProcessor;
        }

        public async Task<PagedList<ShopDto>> Handle(GetShopsQuery query, CancellationToken cancellationToken)
        {
            var baseQuery = await _shopRepository.GetAllShops()
                ?? throw new NotFoundException("Shops not found");

            var sortedQuery = _queryProcessor.SortQuery(baseQuery.AsQueryable(), query.SortBy, query.SortDirection);

            var pagedShops = _queryProcessor.PageQuery(sortedQuery.AsEnumerable(), query.PageNumber, query.PageSize);

            var shopListDto = ShopDto.CreateDtoFromObject(pagedShops);

            var pagedShopList = new PagedList<ShopDto>(shopListDto,
                                                       baseQuery.Count(),
                                                       query.PageNumber,
                                                       query.PageSize);

            return pagedShopList;
        }
    }
}
