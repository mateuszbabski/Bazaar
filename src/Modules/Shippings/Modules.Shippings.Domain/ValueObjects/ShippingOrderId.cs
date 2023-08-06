using Modules.Shippings.Domain.Exceptions;

namespace Modules.Shippings.Domain.ValueObjects
{
    public record ShippingOrderId
    {
        public Guid Value { get; }

        public ShippingOrderId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidShippingOrderIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ShippingOrderId id) => id.Value;
        public static implicit operator ShippingOrderId(Guid value) => new(value);
    }
}
