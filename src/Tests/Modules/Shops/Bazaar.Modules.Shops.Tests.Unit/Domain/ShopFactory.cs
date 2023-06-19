using Modules.Shops.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Shops.Tests.Unit.Domain
{
    public class ShopFactory
    {
        public static Shop GetShop()
        {
            var address = Address.CreateAddress("country", "city", "street", "postalCode");

            var shop = Shop.Create("shop@example.com",
                                   "passwordHash",
                                   "ownerName",
                                   "ownerLastName",
                                   "shopName",
                                   address,
                                   "taxNumber",
                                   "telephoneNumber");

            return shop;
        }
    }
}
