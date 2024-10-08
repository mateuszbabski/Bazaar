﻿using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.ValueObjects;
using Shared.Domain;

namespace Modules.Discounts.Domain.Events
{
    public sealed record NewDiscountCouponAddedToListDomainEvent(string DiscountCode, DiscountTarget DiscountTarget) 
        : IDomainEvent
    {
    }
}
