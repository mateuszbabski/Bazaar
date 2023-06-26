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

        public static List<Shop> GetShopsList()
        {
            var address1 = Address.CreateAddress("Poland", "Warsaw", "street", "postalCode");

            var shop1 = Shop.Create("shop@example.com",
                                   "passwordHash",
                                   "ownerName",
                                   "ownerLastName",
                                   "Animal Shop",
                                   address1,
                                   "taxNumber",
                                   "telephoneNumber");

            var address2 = Address.CreateAddress("Poland", "Gdansk", "street", "postalCode");

            var shop2 = Shop.Create("shop2@example.com",
                                   "passwordHash",
                                   "ownerName",
                                   "ownerLastName",
                                   "Food Shop",
                                   address2,
                                   "taxNumber",
                                   "telephoneNumber");

            var shopList = new List<Shop>()
            {
                shop1,
                shop2,
            };

            return shopList;
        }
    }
}
