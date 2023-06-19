using Modules.Shops.Domain.Entities;
using Shared.Domain.Exceptions;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Shops.Tests.Unit.Domain
{
    public class ShopDomainTest
    {
        [Fact]
        public void CreateShop_ReturnsShopIfParamsAreValid()
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

            Assert.NotNull(shop);
            Assert.IsType<Shop>(shop);
            Assert.Equal("shop@example.com", shop.Email);
        }

        [Fact]
        public void CreateShop_ThrowsInvalidEmailExceptionWhenEmailIsInvalid()
        {
            var address = Address.CreateAddress("country", "city", "street", "postalCode");

            var act = Assert.Throws<InvalidEmailException>(() => Shop.Create("",
                                                                             "passwordHash",
                                                                             "ownerName",
                                                                             "ownerLastName",
                                                                             "shopName",
                                                                             address,
                                                                             "taxNumber",
                                                                             "telephoneNumber"));

            Assert.IsType<InvalidEmailException>(act);
        }

        [Fact]
        public void UpdateShopDetails_ReturnsUpdatedShopDetailsIfParamsAreValid()
        {
            var shop = GetShop();
            shop.UpdateShopDetails("updatedOwnerName",
                                   null,
                                   null,
                                   null,
                                   null,
                                   null,
                                   null,
                                   null,
                                   null);

            Assert.NotNull(shop);
            Assert.IsType<Shop>(shop);
            Assert.Equal("updatedOwnerName", shop.OwnerName);
        }

        [Fact]
        public void UpdateShopDetails_ReturnsOriginalShopDataIfParamsAreNull()
        {
            var shop = GetShop();

            shop.UpdateShopDetails(null,
                                   null,
                                   null,
                                   null,
                                   null,
                                   null,
                                   null,
                                   null,
                                   null);

            Assert.NotNull(shop);
            Assert.IsType<Shop>(shop);
            Assert.Equal("shopName", shop.ShopName);
        }

        private static Shop GetShop()
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
