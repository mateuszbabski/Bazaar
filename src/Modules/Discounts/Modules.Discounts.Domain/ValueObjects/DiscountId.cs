namespace Modules.Discounts.Domain.ValueObjects
{
    public record DiscountId
    {
        public Guid Value { get; }

        public DiscountId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidDiscountIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(DiscountId id) => id.Value;
        public static implicit operator DiscountId(Guid value) => new(value);
    }
}
