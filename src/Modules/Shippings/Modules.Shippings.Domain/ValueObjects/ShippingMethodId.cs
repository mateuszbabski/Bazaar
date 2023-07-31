using Modules.Shippings.Domain.Exceptions;

namespace Modules.Shippings.Domain.ValueObjects
{
    public record ShippingMethodId
    {
        public Guid Value { get; }

        public ShippingMethodId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidShippingMethodIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ShippingMethodId id) => id.Value;
        public static implicit operator ShippingMethodId(Guid value) => new(value);
    }
}
