using Modules.Orders.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Orders.Domain.Entities
{
    public class OrderItem : Entity
    {
        public OrderItemId Id { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid ShopId { get; private set; }
        public OrderId OrderId { get; private set; }
        public int Quantity { get; private set; } = 1;
        public MoneyValue Price { get; private set; }

        private OrderItem() { }

        internal OrderItem(Guid productId,
                           Guid shopId,
                           OrderId orderId,
                           int quantity,
                           MoneyValue productPrice
                           ) 
        {
            Id = new OrderItemId(Guid.NewGuid());
            ProductId = productId;
            OrderId = orderId;
            ShopId = shopId;
            Quantity = quantity;
            Price = CountOrderItemPrice(quantity, productPrice.Amount, productPrice.Currency);
        }

        public static OrderItem CreateOrderItemFromProduct(
                                                        Guid productId,
                                                        Guid shopId,
                                                        OrderId orderId,
                                                        int quantity,
                                                        MoneyValue productPrice)
        {
            return new OrderItem(productId, shopId, orderId, quantity, productPrice);
        }

        private static MoneyValue CountOrderItemPrice(int quantity, decimal price, string currency)
        {
            return MoneyValue.Of(quantity * price, currency);
        }
    }
}
