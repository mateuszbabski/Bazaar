using Modules.Shippings.Domain.Events;
using Modules.Shippings.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Shippings.Domain.Entities
{
    public class Shipping : Entity, IAggregateRoot
    {
        public ShippingId Id { get; private set; }
        public ShippingOrderId OrderId { get; private set; }
        public ShippingMethodId ShippingMethodId { get; private set; }
        public TrackingNumber TrackingNumber { get; private set; }
        public ShippingStatus Status { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.Now;
        public DateTimeOffset LastUpdateDate { get; private set; } = DateTimeOffset.Now;
        public Weight TotalWeight { get; private set; } 
        public MoneyValue TotalPrice { get; private set; }

        private Shipping()
        {
            
        }

        internal Shipping(ShippingOrderId orderId,
                          ShippingMethodId methodId,
                          DateTimeOffset createdOn,
                          decimal weight,
                          MoneyValue baseShippingPrice)
        {
            Id = new ShippingId(Guid.NewGuid());
            OrderId = orderId;
            ShippingMethodId = methodId;
            TrackingNumber = new TrackingNumber(Guid.NewGuid());
            Status = ShippingStatus.Pending;
            CreatedDate = createdOn;
            LastUpdateDate = createdOn;
            TotalWeight = weight;
            TotalPrice = CalculateTotalPrice(weight, baseShippingPrice);
        }

        public static Shipping CreateShipping(ShippingOrderId orderId,
                                              ShippingMethodId methodId,
                                              DateTimeOffset createdOn,
                                              decimal weight,
                                              MoneyValue baseShippingPrice)
        {
            var shipping = new Shipping(orderId, methodId, createdOn, weight, baseShippingPrice);
            shipping.AddDomainEvent(new ShippingCreatedDomainEvent(shipping.Id));
            return shipping;
        }

        private MoneyValue CalculateTotalPrice(decimal weight, MoneyValue baseShippingPrice)
        {            
            return TotalPrice;
        }
    }

}
