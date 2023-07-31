using Modules.Shippings.Domain.Exceptions;

namespace Modules.Shippings.Domain.ValueObjects
{
    public record TrackingNumber
    {
        // TODO: create tracking number
        public string Value { get; }

        public TrackingNumber(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidTrackingNumberException();
            }

            Value = value.ToString();
        }

        public static implicit operator string(TrackingNumber trackingNumber) => trackingNumber.Value;
        public static implicit operator TrackingNumber(string value) => new(value);
    }
}
