﻿using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.Repositories;

namespace Modules.Shippings.Infrastructure.Repository
{
    internal sealed class ShippingRepository : IShippingRepository
    {
        // TODO: Repository for shippings
        public Task<Shipping> CreateShipping(Shipping shipping)
        {
            throw new NotImplementedException();
        }

        public Task CancelShipping(Shipping shipping)
        {
            throw new NotImplementedException();
        }

        public Task<Shipping> GetShippingById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Shipping> GetShippingByOrderId(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Shipping> GetShippingByTrackingNumber(Guid trackingNumber)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Shipping>> GetShippings()
        {
            throw new NotImplementedException();
        }
    }
}
