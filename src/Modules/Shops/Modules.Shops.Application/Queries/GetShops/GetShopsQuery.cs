using MediatR;
using Modules.Shops.Application.Dtos;

namespace Modules.Shops.Application.Queries.GetShops
{
    public class GetShopsQuery : IRequest<IEnumerable<ShopDto>>
    {
    }
}
