using Modules.Baskets.Domain.Exceptions;

namespace Modules.Baskets.Domain.ValueObjects
{
    public record BasketId
    {
        public Guid Value { get; }

        public BasketId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidBasketIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(BasketId id) => id.Value;
        public static implicit operator BasketId(Guid value) => new(value);
    }
}
