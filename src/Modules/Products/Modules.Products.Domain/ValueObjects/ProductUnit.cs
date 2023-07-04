using Modules.Products.Domain.Exceptions;

namespace Modules.Products.Domain.ValueObjects
{
    public record ProductUnit
    {
        public string Value { get; }
        public ProductUnit(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidProductUnitException();
            }

            Value = value;
        }

        public static implicit operator ProductUnit(string unit) => new(unit);
        public static implicit operator string(ProductUnit unit) => unit.Value;
    }
}
