namespace Modules.Discounts.Domain.ValueObjects
{
    public record DiscountCode
    {
        public string Value { get; }

        public DiscountCode(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidDiscountCodeException();
            }

            Value = value.ToString().Substring(0, 12);
        }

        public static implicit operator string(DiscountCode id) => id.Value;
        public static implicit operator DiscountCode(string value) => new(value);
    }
}
