using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCouponById;
using Modules.Discounts.Application.Queries.Discounts.GetDiscountById;
using Modules.Discounts.Domain.Repositories;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class GetDiscountCouponByIdQueryTest
    {
        private readonly GetDiscountCouponByIdQueryHandler _sut;
        private readonly IDiscountCouponRepository _discountCouponRepository = Substitute.For<IDiscountCouponRepository>();
        public GetDiscountCouponByIdQueryTest()
        {
            _sut = new GetDiscountCouponByIdQueryHandler(_discountCouponRepository);
        }

        [Fact]
        public async Task GetCouponById_ReturnsCoupon_IfExists()
        {
            var couponList = DiscountCouponsListMock.GetDiscountCoupons(Guid.NewGuid(), Guid.NewGuid());

            var query = new GetDiscountCouponByIdQuery()
            {
                Id = couponList[0].Id
            };

            _discountCouponRepository.GetDiscountCouponById(query.Id).Returns(couponList[0]);

            var result = await _sut.Handle(query, CancellationToken.None);

            await _discountCouponRepository.Received().GetDiscountCouponById(query.Id);

            Assert.IsType<DiscountCouponDto>(result);
            Assert.Equal(query.Id, result.Id);
        }

        [Fact]
        public async Task GetCouponById_ThrowsNotFound_IfDoesntExist()
        {
            var query = new GetDiscountCouponByIdQuery()
            {
                Id = Guid.NewGuid()
            };

            _discountCouponRepository.GetDiscountCouponById(query.Id).Throws(new NotFoundException("Discount Coupon not found"));

            var act = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(query, CancellationToken.None));

            await _discountCouponRepository.Received().GetDiscountCouponById(query.Id);

            Assert.IsType<NotFoundException>(act);
        }
    }
}
