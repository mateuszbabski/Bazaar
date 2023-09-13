using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCouponByCode;
using Modules.Discounts.Domain.Repositories;
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
        public async Task GetCouponById_ReturnsCoupon_IfExists()
        {
            var couponList = DiscountCouponsListMock.GetDiscountCoupons(Guid.NewGuid(), Guid.NewGuid());

            var query = new GetDiscountCouponByCodeQuery()
            {
                DiscountCode = couponList[0].DiscountCode
            };

            _discountCouponRepository.GetDiscountByCouponCode(query.DiscountCode).Returns(couponList[0]);

            var result = await _sut.Handle(query, CancellationToken.None);

            await _discountCouponRepository.Received().GetDiscountByCouponCode(query.DiscountCode);

            Assert.IsType<DiscountCouponDto>(result);
            Assert.Equal(query.DiscountCode, result.DiscountCode);
        }

        [Fact]
        public async Task GetCouponById_ThrowsNotFound_IfDoesntExist()
        {
            var query = new GetDiscountCouponByCodeQuery()
            {
                DiscountCode = "XXXXXXXX"
            };

            _discountCouponRepository.GetDiscountByCouponCode(query.DiscountCode).Throws(new NotFoundException("Discount Coupon not found"));

            var act = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(query, CancellationToken.None));

            await _discountCouponRepository.Received().GetDiscountByCouponCode(query.DiscountCode);

            Assert.IsType<NotFoundException>(act);
        }
    }
}
