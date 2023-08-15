using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.Exceptions;
using Shared.Domain.Exceptions;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Shippings.Tests.Unit.ShippingMethods.Domain
{
    public class ShippingMethodDomainTest
    {
        [Fact]
        public void CreateShippingMethod_ReturnsShippingMethod_IfParamsValid()
        {
            var shippingMethod = ShippingMethod.CreateNewShippingMethod("method1", MoneyValue.Of(10, "USD"), 1);

            Assert.NotNull(shippingMethod);
            Assert.IsType<ShippingMethod>(shippingMethod);
            Assert.Equal("method1", shippingMethod.Name);
        }

        [Fact]
        public void CreateShippingMethod_ThrowsInvalidName_IfNameIsInvalid()
        {
            var act = Assert.Throws<InvalidShippingMethodNameException>(() 
                => ShippingMethod.CreateNewShippingMethod("", MoneyValue.Of(10, "USD"), 1));

            Assert.IsType<InvalidShippingMethodNameException>(act);
        }

        [Fact]
        public void CreateShippingMethod_ThrowsInvalidPrice_IfPriceIsInvalid()
        {
            var act = Assert.Throws<InvalidPriceException>(()
                => ShippingMethod.CreateNewShippingMethod("method1", MoneyValue.Of(0, "USD"), 1));

            Assert.IsType<InvalidPriceException>(act);
        }

        [Fact]
        public void CreateShippingMethod_ThrowsInvalidDuration_IfDurationIsInvalid()
        {
            var act = Assert.Throws<InvalidDurationTimeException>(()
                => ShippingMethod.CreateNewShippingMethod("method1", MoneyValue.Of(10, "USD"), 0));

            Assert.IsType<InvalidDurationTimeException>(act);
        }

        [Fact]
        public void UpdateShippingMethodDetails_DetailsUpdated_IfParamsAreValid()
        {
            var shippingMethod = ShippingMethodFactory.GetShippingMethod();

            Assert.Equal("method1", shippingMethod.Name);

            shippingMethod.UpdateDetails("updated name", 5, "USD", 2);
            Assert.Equal("updated name", shippingMethod.Name);
            Assert.Equal(5, shippingMethod.BasePrice.Amount);
        }

        [Fact]
        public void UpdateShippingMethodDetails_PriceUnchanged_IfMoneyParamsAreInvalid()
        {
            var shippingMethod = ShippingMethodFactory.GetShippingMethod();

            Assert.Equal("method1", shippingMethod.Name);

            shippingMethod.UpdateDetails("updated name", 0, null, 0);
            Assert.Equal("updated name", shippingMethod.Name);
            Assert.Equal(10, shippingMethod.BasePrice.Amount);
            Assert.Equal("USD", shippingMethod.BasePrice.Currency);
            Assert.Equal(1, shippingMethod.DurationTimeInDays);
        }
    }
}
