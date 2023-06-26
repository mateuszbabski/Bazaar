using MediatR;
using Modules.Shops.Application.Dtos;

namespace Modules.Shops.Application.Queries.GetShopsByName
{
    public class GetShopsByNameQuery : IRequest<IEnumerable<ShopDto>>
    {
        #nullable enable
        public string? ShopName { get; set; }
    }
}
