using Shared.Domain.Exceptions;

namespace Shared.Domain.ValueObjects
{
    public record Weight
    {
        public decimal Value { get; }

        public Weight(decimal value)
        {
            if (value < 0 || string.IsNullOrEmpty(value.ToString()))
            {
                throw new InvalidWeightException();
            }

            Value = value;
        }

        public override string ToString() => $"{Value} kg";

        public static implicit operator decimal(Weight weight) => weight.Value;
        public static implicit operator Weight(decimal value) => new(value);
    }
}
