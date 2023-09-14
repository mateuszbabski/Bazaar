using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCouponByCode;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class GetDiscountCouponByCodeQueryTest
    {
        private readonly GetDiscountCouponByCodeQueryHandler _sut;
        private readonly IDiscountCouponRepository _discountCouponRepository = Substitute.For<IDiscountCouponRepository>();
        public GetDiscountCouponByCodeQueryTest()
        {
            _sut = new GetDiscountCouponByCodeQueryHandler(_discountCouponRepository);
        }

        [Fact]
        public async Task GetCouponByCode_ReturnsCoupon_IfCouponExists()
        {
            var customerId = Guid.NewGuid();
            var couponList = DiscountCouponsListMock.GetDiscountCoupons(Guid.NewGuid(), customerId);

            var query = new GetDiscountCouponByCodeQuery()
            {
                DiscountCode = couponList[0].DiscountCode
            };

            _discountCouponRepository.GetDiscountCouponByCouponCode(query.DiscountCode).Returns(couponList[0]);

            var result = await _sut.Handle(query, CancellationToken.None);

            Assert.IsType<DiscountCouponDto>(result);
        }

        [Fact]
        public async Task GetCouponByCode_ThrowsNotFound_IfNotExist()
        {
            var query = new GetDiscountCouponByCodeQuery()
            {
                DiscountCode = "XXXXXXXX"
            };

            _discountCouponRepository.GetDiscountCouponByCouponCode(query.DiscountCode)
                                     .Throws(new NotFoundException("Discount Coupon not found"));

            var act = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(query, CancellationToken.None));

            Assert.IsType<NotFoundException>(act);
        }
    }
}
