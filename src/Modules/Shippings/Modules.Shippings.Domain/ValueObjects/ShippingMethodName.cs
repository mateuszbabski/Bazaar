using Modules.Shippings.Domain.Exceptions;

namespace Modules.Shippings.Domain.ValueObjects
{
    public record ShippingMethodName
    {
        public string Value { get; }
        public ShippingMethodName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidShippingMethodNameException();
            }

            Value = value;
        }

        public static implicit operator string(ShippingMethodName shippingMethodName) => shippingMethodName.Value;
        public static implicit operator ShippingMethodName(string value) => new(value);
    }
}
