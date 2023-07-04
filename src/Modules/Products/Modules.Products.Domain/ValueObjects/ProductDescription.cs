using Modules.Products.Domain.Exceptions;

namespace Modules.Products.Domain.ValueObjects
{
    public record ProductDescription
    {
        public string Value { get; }
        public ProductDescription(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidProductDescriptionException();
            }

            Value = value;
        }

        public static implicit operator string(ProductDescription productDescription) => productDescription.Value;
        public static implicit operator ProductDescription(string value) => new(value);
    }
}
