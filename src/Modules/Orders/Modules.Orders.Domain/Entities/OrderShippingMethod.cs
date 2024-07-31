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

        private OrderShippingMethod(Name name, MoneyValue price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
        }

        public static OrderShippingMethod CreateNewShippingMethod(string name, decimal amount, string currency)
        {
            var shippingMethod = new OrderShippingMethod(name, MoneyValue.Of(amount, currency));

            return shippingMethod;
        }
    }
}
