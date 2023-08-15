using MediatR;
using Modules.Shippings.Application.Dtos;

namespace Modules.Shippings.Application.Queries.ShippingMethods.GetShippingMethodById
{
    public class GetShippingMethodByIdQuery : IRequest<ShippingMethodDto>
    {
        public Guid Id { get; set; }    
    }
}
