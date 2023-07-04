using Modules.Products.Domain.Exceptions;

namespace Modules.Products.Domain.ValueObjects
{
    public record ProductName
    {
        public string Value { get; }
        public ProductName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidProductNameException();
            }

            Value = value;
        }

        public static implicit operator string(ProductName productName) => productName.Value;
        public static implicit operator ProductName(string value) => new(value);
    }
}
