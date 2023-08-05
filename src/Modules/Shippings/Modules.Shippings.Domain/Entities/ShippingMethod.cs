using Modules.Shippings.Domain.Events;
using Modules.Shippings.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Shippings.Domain.Entities
{
    public class ShippingMethod : Entity
    {
        public ShippingMethodId Id { get; private set; }
        public ShippingMethodName Name { get; private set; }
        public MoneyValue BasePrice { get; private set; }
        public int DurationTimeInDays { get; private set; } = 1;
        public bool IsAvailable { get; private set; } = true;

        private ShippingMethod()
        {
            
        }

        internal ShippingMethod(ShippingMethodName name, MoneyValue basePrice, int durationTime)
        {
            Id = new ShippingMethodId(Guid.NewGuid());
            Name = name;
            BasePrice = basePrice;
            DurationTimeInDays = durationTime;
        }

        public static ShippingMethod CreateNewShippingMethod(string name, MoneyValue basePrice, int durationTime)
        {
            var shippingMethod = new ShippingMethod(name, basePrice, durationTime);

            shippingMethod.AddDomainEvent(new NewShippingMethodCreatedDomainEvent(shippingMethod.Id));

            return shippingMethod;
        }
    }
}
