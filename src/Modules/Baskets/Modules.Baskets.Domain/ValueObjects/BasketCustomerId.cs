using Modules.Baskets.Domain.Exceptions;

namespace Modules.Baskets.Domain.ValueObjects
{
    public record BasketCustomerId
    {
        public Guid Value { get; }

        public BasketCustomerId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidBasketCustomerIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(BasketCustomerId id) => id.Value;
        public static implicit operator BasketCustomerId(Guid value) => new(value);
    }
}
