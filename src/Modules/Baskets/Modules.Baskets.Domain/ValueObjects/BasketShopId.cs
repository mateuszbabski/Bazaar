using Modules.Baskets.Domain.Exceptions;

namespace Modules.Baskets.Domain.ValueObjects
{
    public record BasketShopId
    {
        public Guid Value { get; }

        public BasketShopId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidBasketShopIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(BasketShopId id) => id.Value;
        public static implicit operator BasketShopId(Guid value) => new(value);
    }
}
