using Modules.Orders.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Modules.Orders.Application.Dtos
{
    public record OrderItemDto
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public Guid ShopId { get; init; }
        public Guid OrderId { get; init; }
        public int Quantity { get; init; }
        public MoneyValue Price { get; init; }

        public static OrderItemDto CreateOrderItemDtoFromObject(OrderItem orderItem)
        {
            return new OrderItemDto()
            {
                Id = orderItem.Id,
                ProductId = orderItem.ProductId,
                ShopId = orderItem.ShopId,
                OrderId = orderItem.OrderId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };
        }
    }
}
