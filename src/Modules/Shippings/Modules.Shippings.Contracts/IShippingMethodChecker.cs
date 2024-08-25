using Modules.Shippings.Domain.Entities;

namespace Modules.Shippings.Contracts
{
    public interface IShippingMethodChecker
    {
        Task<ShippingMethod> GetShippingMethodByItsName(string methodName);
    }
}
