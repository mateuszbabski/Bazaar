using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Domain.ValueObjects
{
    public record OrderId
    {
        public Guid Value { get; }

        public OrderId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidOrderIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(OrderId id) => id.Value;
        public static implicit operator OrderId(Guid value) => new(value);
    }
}
