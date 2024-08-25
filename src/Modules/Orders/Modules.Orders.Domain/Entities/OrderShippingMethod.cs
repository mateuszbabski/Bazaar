using MediatR.NotificationPublishers;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Orders.Domain.Entities
{
    public class OrderShippingMethod : Entity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public MoneyValue Price { get; private set; }

        private OrderShippingMethod()
        {
        }

        private OrderShippingMethod(Guid id, Name name, MoneyValue price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public static OrderShippingMethod CreateNewShippingMethod(Guid id, string name, decimal amount, string currency)
        {
            var shippingMethod = new OrderShippingMethod(id, name, MoneyValue.Of(amount, currency));

            return shippingMethod;
        }
    }
}
