using Modules.Shippings.Domain.Entities;

namespace Modules.Shippings.Domain.Repositories
{
    public interface IShippingMethodRepository
    {
        Task<ShippingMethod> CreateShippingMethod(ShippingMethod shipping);
        Task<ShippingMethod> UpdateShippingMethod(ShippingMethod shipping);
        Task<ShippingMethod> GetShippingMethodById(Guid id);
        Task DeleteShippingMethod(ShippingMethod shippingMethod);
        Task<IEnumerable<ShippingMethod>> GetShippingMethods();
    }
}
