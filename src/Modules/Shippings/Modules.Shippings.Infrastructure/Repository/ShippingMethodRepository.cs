﻿using Microsoft.EntityFrameworkCore;
using Modules.Shippings.Contracts;
using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.Repositories;
using Modules.Shippings.Domain.ValueObjects;
using Modules.Shippings.Infrastructure.Context.ShippingMethods;

namespace Modules.Shippings.Infrastructure.Repository
{
    internal sealed class ShippingMethodRepository : IShippingMethodRepository, IShippingMethodChecker
    {
        private readonly ShippingMethodsDbContext _dbContext;

        public ShippingMethodRepository(ShippingMethodsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ShippingMethod> AddShippingMethod(ShippingMethod shipping)
        {
            await _dbContext.ShippingMethods.AddAsync(shipping);

            return shipping;
        }

        public async Task DeleteShippingMethod(ShippingMethod shippingMethod)
        {
            _dbContext.ShippingMethods.Remove(shippingMethod);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ShippingMethod> GetShippingMethodById(ShippingMethodId id)
        {
            return await _dbContext.ShippingMethods.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ShippingMethod> GetShippingMethodByItsName(string methodName)
        {
            return await GetShippingMethodByName(methodName);
        }

        public async Task<ShippingMethod> GetShippingMethodByName(ShippingMethodName methodName)
        {
            return await _dbContext.ShippingMethods.FirstOrDefaultAsync(x => x.Name == methodName);
        }

        public async Task<IEnumerable<ShippingMethod>> GetShippingMethods()
        {
            return await _dbContext.ShippingMethods.ToListAsync();
        }
    }
}
