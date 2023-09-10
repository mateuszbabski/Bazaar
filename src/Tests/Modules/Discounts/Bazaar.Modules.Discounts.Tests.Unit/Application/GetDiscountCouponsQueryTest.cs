using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCoupons;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shared.Abstractions.Queries;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class GetDiscountCouponsQueryTest
    {
        private readonly GetDiscountCouponsQueryHandler _sut;
        private readonly ICurrentUserService _currentUserService = Substitute.For<ICurrentUserService>();
        private readonly IDiscountCouponRepository _discountCouponRepository = Substitute.For<IDiscountCouponRepository>();
        private readonly IQueryProcessor<DiscountCoupon> _queryProcessor = Substitute.For<IQueryProcessor<DiscountCoupon>>();
        public GetDiscountCouponsQueryTest()
        {
            _sut = new GetDiscountCouponsQueryHandler(_currentUserService, _discountCouponRepository, _queryProcessor);
        }

        [Fact]
        public async Task GetDiscountCoupons_ReturnCoupons_ValidQueryListForAdmin()
        {
            var shopId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var couponList = DiscountCouponsListMock.GetDiscountCoupons(shopId, customerId);

            _currentUserService.UserId.Returns(Guid.NewGuid());
            _currentUserService.UserRole.Returns("admin");
            _discountCouponRepository.GetAll().Returns(Task.FromResult(couponList.AsEnumerable()));

            _queryProcessor.SortQuery(Arg.Any<IQueryable<DiscountCoupon>>(), Arg.Any<string>(), Arg.Any<string>())
                               .Returns(couponList.AsQueryable());

            _queryProcessor.PageQuery(Arg.Any<IEnumerable<DiscountCoupon>>(), Arg.Any<int>(), Arg.Any<int>())
                               .Returns(couponList);

            var result = await _sut.Handle(new GetDiscountCouponsQuery(), CancellationToken.None);

            await _discountCouponRepository.Received().GetAll();

            _queryProcessor.Received()
                           .SortQuery(Arg.Any<IQueryable<DiscountCoupon>>(), Arg.Any<string>(), Arg.Any<string>());

            _queryProcessor.Received()
                           .PageQuery(Arg.Any<IEnumerable<DiscountCoupon>>(), Arg.Any<int>(), Arg.Any<int>());

            Assert.IsAssignableFrom<PagedList<DiscountCouponDto>>(result);
            Assert.Equal(6, result.TotalCount);
        }

        [Fact]
        public async Task GetDiscountCoupons_ReturnCoupons_ValidQueryListForShop()
        {
            var shopId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var couponList = DiscountCouponsListMock.GetDiscountCoupons(shopId, customerId);
            var filteredCouponList = couponList.Where(x => x.CreatedBy == shopId).ToList();
            var queryableCouponList = filteredCouponList.AsQueryable();

            _currentUserService.UserId.Returns(shopId);
            _currentUserService.UserRole.Returns("shop");
            _discountCouponRepository.GetAllByCreator(shopId).Returns(Task.FromResult(filteredCouponList.AsEnumerable()));

            _queryProcessor.SortQuery(Arg.Any<IQueryable<DiscountCoupon>>(), Arg.Any<string>(), Arg.Any<string>())
                               .Returns(queryableCouponList);

            _queryProcessor.PageQuery(Arg.Any<IEnumerable<DiscountCoupon>>(), Arg.Any<int>(), Arg.Any<int>())
                               .Returns(filteredCouponList);

            var result = await _sut.Handle(new GetDiscountCouponsQuery(), CancellationToken.None);

            await _discountCouponRepository.Received().GetAllByCreator(Arg.Any<Guid>());

            _queryProcessor.Received()
                           .SortQuery(Arg.Any<IQueryable<DiscountCoupon>>(), Arg.Any<string>(), Arg.Any<string>());

            _queryProcessor.Received()
                           .PageQuery(Arg.Any<IEnumerable<DiscountCoupon>>(), Arg.Any<int>(), Arg.Any<int>());

            Assert.IsAssignableFrom<PagedList<DiscountCouponDto>>(result);
            Assert.Equal(4, result.TotalCount);
        }

        [Fact]
        public async Task GetDiscountCoupons_ReturnCoupons_ValidQueryListForCustomer()
        {
            var shopId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var discountList = DiscountCouponsListMock.GetDiscounts(shopId, customerId);
            var filteredCouponList = discountList.Where(x => x.DiscountTarget.TargetId == customerId).SelectMany(x => x.DiscountCoupons).ToList();
            var queryableCouponList = filteredCouponList.AsQueryable();

            _currentUserService.UserId.Returns(customerId);
            _currentUserService.UserRole.Returns("customer");
            _discountCouponRepository.GetAllTargetedForCustomer(customerId).Returns(Task.FromResult(filteredCouponList.AsEnumerable()));

            _queryProcessor.SortQuery(Arg.Any<IQueryable<DiscountCoupon>>(), Arg.Any<string>(), Arg.Any<string>())
                               .Returns(queryableCouponList);

            _queryProcessor.PageQuery(Arg.Any<IEnumerable<DiscountCoupon>>(), Arg.Any<int>(), Arg.Any<int>())
                               .Returns(filteredCouponList);

            var result = await _sut.Handle(new GetDiscountCouponsQuery(), CancellationToken.None);

            await _discountCouponRepository.Received().GetAllTargetedForCustomer(Arg.Any<Guid>());

            _queryProcessor.Received()
                           .SortQuery(Arg.Any<IQueryable<DiscountCoupon>>(), Arg.Any<string>(), Arg.Any<string>());

            _queryProcessor.Received()
                           .PageQuery(Arg.Any<IEnumerable<DiscountCoupon>>(), Arg.Any<int>(), Arg.Any<int>());

            Assert.IsAssignableFrom<PagedList<DiscountCouponDto>>(result);
            Assert.Equal(2, result.TotalCount);
        }

        [Fact]
        public async Task GetDiscountCoupons_ThrowsForbidException_AnonymousUser()
        {
            _currentUserService.UserId.Returns(Guid.Empty);

            var act = await Assert.ThrowsAsync<ForbidException>(() 
                => _sut.Handle(new GetDiscountCouponsQuery(), CancellationToken.None));

            Assert.IsType<ForbidException>(act);
        }

        [Fact]
        public async Task GetDiscountCoupons_ThrowsForbidException_ShopIdInvalid()
        {
            var shopId = Guid.NewGuid();
            _currentUserService.UserId.Returns(shopId);
            _currentUserService.UserRole.Returns("shop");

            _discountCouponRepository.GetAllByCreator(shopId).ThrowsAsync(new ForbidException("You are now allowed to proceed this action"));

            var act = await Assert.ThrowsAsync<ForbidException>(()
                => _sut.Handle(new GetDiscountCouponsQuery(), CancellationToken.None));

            Assert.IsType<ForbidException>(act);
        }

        [Fact]
        public async Task GetDiscountCoupons_ThrowsForbidException_CustomerIdInvalid()
        {
            var customerId = Guid.NewGuid();
            _currentUserService.UserId.Returns(customerId);
            _currentUserService.UserRole.Returns("customer");

            _discountCouponRepository.GetAllTargetedForCustomer(customerId).ThrowsAsync(new ForbidException("You are now allowed to proceed this action"));

            var act = await Assert.ThrowsAsync<ForbidException>(()
                => _sut.Handle(new GetDiscountCouponsQuery(), CancellationToken.None));

            Assert.IsType<ForbidException>(act);
        }
    }
}
