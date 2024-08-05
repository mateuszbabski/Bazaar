using MediatR;
using Modules.Orders.Application.Dtos;

namespace Modules.Orders.Application.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
