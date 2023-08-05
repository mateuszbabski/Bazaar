using Modules.Shippings.Domain.Exceptions;

namespace Modules.Shippings.Domain.ValueObjects
{
    public record TrackingNumber
    {
        // TODO: create tracking number
        public Guid Value { get; }

        public TrackingNumber(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidTrackingNumberException();
            }
        }

        public static implicit operator Guid(TrackingNumber trackingNumber) => trackingNumber.Value;
        public static implicit operator TrackingNumber(Guid value) => new(value);
    }
}
