using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Modules.Orders.Application.Dtos
{
    public record OrderDetailsDto
    {
        public Guid Id { get; init; }
        public Receiver Receiver { get; init; }
        public List<OrderItemDto> Items { get; init; }
        public MoneyValue TotalPrice { get; init; }
        public Weight TotalWeight { get; init; }
        public OrderShippingMethod OrderShippingMethod { get; init; }
        public string OrderStatus { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public DateTimeOffset LastUpdateDate { get; init; }
        public static OrderDetailsDto CreateOrderDtoFromObject(Order order)
        {
            var orderItemsList = new List<OrderItemDto>();

            foreach (var item in order.Items)
            {
                var orderItemDto = OrderItemDto.CreateOrderItemDtoFromObject(item);

                orderItemsList.Add(orderItemDto);
            }

            return new OrderDetailsDto()
            {
                Id = order.Id,
                Receiver = order.Receiver,
                TotalPrice = order.TotalPrice,
                TotalWeight = order.TotalWeight,
                Items = orderItemsList,
                OrderShippingMethod = order.OrderShippingMethod,
                OrderStatus = order.OrderStatus.ToString(),
                CreatedDate = order.CreatedDate,
                LastUpdateDate = order.LastUpdateDate
            };
        }
    }
}
