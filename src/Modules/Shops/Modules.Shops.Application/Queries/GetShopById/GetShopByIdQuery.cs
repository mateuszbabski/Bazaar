using MediatR;
using Modules.Shops.Application.Dtos;

namespace Modules.Shops.Application.Queries.GetShopById
{
    public class GetShopByIdQuery : IRequest<ShopDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
