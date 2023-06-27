using MediatR;
using Modules.Shops.Application.Dtos;
using Shared.Application.Queries;

namespace Modules.Shops.Application.Queries.GetShopsByName
{
    public class GetShopsByNameQuery : IRequest<PagedList<ShopDto>>
    {
        #nullable enable
        public string? ShopName { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
