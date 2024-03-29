﻿using Modules.Customers.Domain.Exceptions;

namespace Modules.Customers.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; }

        public CustomerId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyCustomerIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(CustomerId id) => id.Value;
        public static implicit operator CustomerId(Guid value) => new(value);
    }
}
