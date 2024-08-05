using MediatR;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Domain.Repositories;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Orders.Application.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDetailsDto>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(ICurrentUserService currentUserService, IOrderRepository orderRepository)
        {
            _currentUserService = currentUserService;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDetailsDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var userRole = _currentUserService.UserRole;

            var order = await _orderRepository.GetOrderById(request.Id)
                ?? throw new NotFoundException("Order not found.");

            if (userRole == "customer" && order.Receiver.Id.Value != userId || userRole == "shop")
            {
                throw new ForbidException("You are not authorized to view this order.");
            }

            var orderDto = OrderDetailsDto.CreateOrderDtoFromObject(order);

            return orderDto;
        }
    }
}
