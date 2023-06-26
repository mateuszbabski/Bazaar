using MediatR;
using Modules.Shops.Application.Dtos;

namespace Modules.Shops.Application.Queries.GetShopsByLocalization
{
    public class GetShopsByLocalizationQuery : IRequest<IEnumerable<ShopDto>>
    {
        public string Country { get; set; }
        public string City { get; set; }
    }
}
