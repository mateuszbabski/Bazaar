﻿using Modules.Shippings.Domain.ValueObjects;
using Shared.Domain;

namespace Modules.Shippings.Domain.Events
{
    public sealed record NewShippingMethodCreatedDomainEvent(ShippingMethodId Id) : IDomainEvent
    {
    }
}
