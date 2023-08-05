using Modules.Shippings.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
