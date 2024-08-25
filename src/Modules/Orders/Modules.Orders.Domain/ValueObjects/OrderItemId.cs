using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Domain.ValueObjects
{
    public record OrderItemId
    {
        public Guid Value { get; }

        public OrderItemId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidOrderItemIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(OrderItemId id) => id.Value;
        public static implicit operator OrderItemId(Guid value) => new(value);
    }
}
