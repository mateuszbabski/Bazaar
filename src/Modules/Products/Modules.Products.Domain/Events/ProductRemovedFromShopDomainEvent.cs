﻿using Modules.Products.Domain.Entities;
using Shared.Domain;

namespace Modules.Products.Domain.Events
{
    public record ProductRemovedFromShopDomainEvent(Product Product) : IDomainEvent
    {
    }
}
