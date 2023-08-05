using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.Repositories;

namespace Modules.Shippings.Infrastructure.Repository
{
    internal sealed class ShippingMethodRepository : IShippingMethodRepository
    {
        public Task<ShippingMethod> CreateShippingMethod(ShippingMethod shipping)
        {
            throw new NotImplementedException();
        }

        public Task DeleteShippingMethod(ShippingMethod shippingMethod)
        {
            throw new NotImplementedException();
        }

        public Task<ShippingMethod> GetShippingMethodById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShippingMethod>> GetShippingMethods()
        {
            throw new NotImplementedException();
        }

        public Task<ShippingMethod> UpdateShippingMethod(ShippingMethod shipping)
        {
            throw new NotImplementedException();
        }
    }
}
