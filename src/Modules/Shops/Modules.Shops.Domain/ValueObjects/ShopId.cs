﻿using Modules.Shops.Domain.Exceptions;

namespace Modules.Shops.Domain.ValueObjects
{
    public record ShopId
    {
        public Guid Value { get; }

        public ShopId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyShopIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ShopId id) => id.Value;
        public static implicit operator ShopId(Guid value) => new(value);
    }
}
