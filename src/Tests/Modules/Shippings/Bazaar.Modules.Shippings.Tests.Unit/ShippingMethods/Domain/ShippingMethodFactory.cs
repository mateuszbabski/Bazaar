using Modules.Shippings.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Shippings.Tests.Unit.ShippingMethods.Domain
{
    public class ShippingMethodFactory
    {
        public static ShippingMethod GetShippingMethod()
        {
            var shippingMethod = ShippingMethod.CreateNewShippingMethod("method1", MoneyValue.Of(10, "USD"), 1);
            return shippingMethod;
        }
    }
}
