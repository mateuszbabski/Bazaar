using Modules.Shippings.Domain.Entities;

namespace Modules.Shippings.Domain.Repositories
{
    public interface IShippingMethodRepository
    {
        Task<ShippingMethod> AddShippingMethod(ShippingMethod shipping);
        Task DeleteShippingMethod(ShippingMethod shippingMethod);
        Task<ShippingMethod> GetShippingMethodById(Guid id);
        Task<IEnumerable<ShippingMethod>> GetShippingMethods();
    }
}
