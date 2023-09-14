using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Application.Queries.Discounts.GetDiscountByCouponCode;
using Modules.Discounts.Domain.Repositories;
using NSubstitute;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class GetDiscountByCouponCodeQueryTest
    {
        private readonly GetDiscountByCouponCodeQueryHandler _sut;
        private readonly IDiscountRepository _discountRepository = Substitute.For<IDiscountRepository>();
        public GetDiscountByCouponCodeQueryTest()
        {
            _sut = new GetDiscountByCouponCodeQueryHandler(_discountRepository);
        }

        [Fact]
        public async Task GetDiscountByCode_ReturnsDiscount_IfExists()
        {
            var customerId = Guid.NewGuid();
            var discountList = DiscountCouponsListMock.GetDiscounts(Guid.NewGuid(), customerId);

            var discount = discountList[0];
            var coupon = discount.DiscountCoupons[0];

            var query = new GetDiscountByCouponCodeQuery()
            {
                CouponCode = coupon.DiscountCode,
            };

            _discountRepository.GetDiscountByCouponCode(query.CouponCode).Returns(discount);

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.IsType<DiscountDto>(result);
            Assert.Equal(discount.Id.Value, result.Id);            
        }
    }
}
