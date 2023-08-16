using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.ValueObjects;

namespace Modules.Shippings.Domain.Repositories
{
    public interface IShippingMethodRepository
    {
        Task<ShippingMethod> AddShippingMethod(ShippingMethod shipping);
        Task DeleteShippingMethod(ShippingMethod shippingMethod);
        Task<ShippingMethod> GetShippingMethodById(ShippingMethodId id);
        Task<IEnumerable<ShippingMethod>> GetShippingMethods();
    }
}
