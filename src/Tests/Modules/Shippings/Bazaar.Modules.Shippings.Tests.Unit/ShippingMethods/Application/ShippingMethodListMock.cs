using Modules.Shippings.Domain.Entities;
using Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazaar.Modules.Shippings.Tests.Unit.ShippingMethods.Application
{
    public class ShippingMethodListMock
    {
        private readonly List<ShippingMethod> _shippingMethodList;
        public List<ShippingMethod> ShippingMethods
        {
            get { return _shippingMethodList; }
        }

        public ShippingMethodListMock()
        {
            var shippingMethod1 = ShippingMethod.CreateNewShippingMethod("name1", MoneyValue.Of(10, "USD"), 1);

            var shippingMethod2 = ShippingMethod.CreateNewShippingMethod("name2", MoneyValue.Of(5, "USD"), 2);

            _shippingMethodList = new List<ShippingMethod>
            {
                shippingMethod1,
                shippingMethod2
            };
        }
    }
}
