using Modules.Baskets.Domain.Exceptions;

namespace Modules.Baskets.Domain.ValueObjects
{
    public record BasketProductId
    {
        public Guid Value { get; }

        public BasketProductId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidBasketProductIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(BasketProductId id) => id.Value;
        public static implicit operator BasketProductId(Guid value) => new(value);
    }
}
