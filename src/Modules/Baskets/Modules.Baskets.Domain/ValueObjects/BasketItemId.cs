using Modules.Baskets.Domain.Exceptions;

namespace Modules.Baskets.Domain.ValueObjects
{
    public record BasketItemId
    {
        public Guid Value { get; }

        public BasketItemId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidBasketItemIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(BasketItemId id) => id.Value;
        public static implicit operator BasketItemId(Guid value) => new(value);
    }
}
