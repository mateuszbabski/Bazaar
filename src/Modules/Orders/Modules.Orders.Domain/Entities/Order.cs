using Modules.Orders.Domain.Events;
using Modules.Orders.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Orders.Domain.Entities
{
    public class Order : Entity, IAggregateRoot
    {
        public OrderId Id { get; private set; }
        public Receiver Receiver { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public MoneyValue TotalPrice { get; private set; }
        public Weight TotalWeight { get; private set; }
        public OrderShippingMethod OrderShippingMethod { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.Now;
        public DateTimeOffset LastUpdateDate { get; private set; } = DateTimeOffset.Now;

        private Order() { }
        private Order(Guid orderId, Receiver receiver, List<OrderItem> orderItems, OrderShippingMethod orderShippingMethod, Weight weight)
        {
            Id = new OrderId(orderId);
            Receiver = receiver;
            Items = orderItems;
            TotalPrice = CountTotalPrice(Items, orderShippingMethod);
            TotalWeight = weight;
            OrderShippingMethod = orderShippingMethod;
            OrderStatus = OrderStatus.Created;
            CreatedDate = DateTimeOffset.Now;
            LastUpdateDate = DateTimeOffset.Now;
        }

        public static Order CreateOrder(Guid orderId, Receiver receiver, List<OrderItem> orderItems, OrderShippingMethod orderShippingMethod, Weight weight)
        {
            var order = new Order(orderId, receiver, orderItems, orderShippingMethod, weight);
            order.AddDomainEvent(new OrderCreatedDomainEvent(order));

            return order;
        }

        private static MoneyValue CountTotalPrice(List<OrderItem> items, OrderShippingMethod shippingMethod)
        {
            decimal allProductsPrice = items.Sum(x => x.Price.Amount);
            decimal shippingPrice = shippingMethod.Price.Amount;

            return new MoneyValue(allProductsPrice + shippingPrice, items.First().Price.Currency);
        }
    }
}
