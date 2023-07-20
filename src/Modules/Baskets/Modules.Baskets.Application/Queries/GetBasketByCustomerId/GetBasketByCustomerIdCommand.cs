using MediatR;
using Modules.Baskets.Application.Dtos;

namespace Modules.Baskets.Application.Queries.GetBasketByCustomerId
{
    public class GetBasketByCustomerIdCommand : IRequest<BasketDto>
    {
    }
}
