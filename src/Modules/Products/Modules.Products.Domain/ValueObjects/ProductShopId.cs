using Modules.Products.Domain.Exceptions;

namespace Modules.Products.Domain.ValueObjects
{
    public record ProductShopId
    {
        public Guid Value { get; }

        public ProductShopId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidProductShopIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ProductShopId id) => id.Value;
        public static implicit operator ProductShopId(Guid value) => new(value);
    }
}
