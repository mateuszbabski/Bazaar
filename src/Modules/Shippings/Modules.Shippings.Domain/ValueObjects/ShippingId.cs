using Modules.Shippings.Domain.Exceptions;

namespace Modules.Shippings.Domain.ValueObjects
{
    public record ShippingId
    {
        public Guid Value { get; }

        public ShippingId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidShippingIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ShippingId id) => id.Value;
        public static implicit operator ShippingId(Guid value) => new(value);
    }
}
