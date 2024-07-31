using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Domain.ValueObjects
{
    public record ReceiverId
    {
        public Guid Value { get; }

        public ReceiverId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidReceiverIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ReceiverId id) => id.Value;
        public static implicit operator ReceiverId(Guid value) => new(value);
    }
}
