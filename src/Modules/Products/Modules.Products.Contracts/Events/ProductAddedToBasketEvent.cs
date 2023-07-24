﻿using Shared.Abstractions.Events;
using Shared.Domain.ValueObjects;

namespace Modules.Products.Contracts.Events
{
    public record ProductAddedToBasketEvent(Guid ProductId,
                                            Guid ShopId,
                                            Guid CustomerId,
                                            MoneyValue ProductPrice,
                                            int Quantity) : IEvent
    {
    }
}
