using Bazaar.Modules.Discounts.Tests.Unit.Domain;
using Modules.Discounts.Application.Commands.DiscountCoupons.DisableDiscountCoupon;
using Modules.Discounts.Application.Contracts;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Exceptions;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class DisableDiscountCouponCommandTest
    {
        private readonly DisableDiscountCouponCommandHandler _sut;
        private readonly IDiscountCouponRepository _discountCouponRepository = Substitute.For<IDiscountCouponRepository>();
        private readonly IDiscountsUnitOfWork _unitOfWork = Substitute.For<IDiscountsUnitOfWork>();
        private readonly ICurrentUserService _userService = Substitute.For<ICurrentUserService>();
        public DisableDiscountCouponCommandTest()
        {
            _sut = new DisableDiscountCouponCommandHandler(_userService, _discountCouponRepository, _unitOfWork);
        }

        [Fact]
        public async Task DisableDiscountCoupon_DisablesDiscountCoupon_AllParamsValid()
        {
            var discount = DiscountFactory.GetValueDiscount();
            var userId = discount.CreatedBy;
            var discountCoupon = discount.CreateNewDiscountCoupon(userId, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(1));
            var command = new DisableDiscountCouponCommand()
            {
                DiscountCouponId = discountCoupon.Id,
            };

            _userService.UserId.Returns(userId);
            _discountCouponRepository.GetDiscountCouponById(command.DiscountCouponId).Returns(discountCoupon);

            Assert.True(discountCoupon.IsEnable);
            await _sut.Handle(command, CancellationToken.None);

            await _discountCouponRepository.Received().GetDiscountCouponById(Arg.Any<DiscountCouponId>());
            await _unitOfWork.Received().CommitChangesAsync();

            Assert.False(discountCoupon.IsEnable);
        }

        [Fact]
        public async Task DisableDiscountCoupon_ThrowsForbidException_IfInvalidUser()
        {
            var userId = Guid.NewGuid();
            var discount = DiscountFactory.GetValueDiscount();
            var discountCoupon = discount.CreateNewDiscountCoupon(discount.CreatedBy, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(1));
            var command = new DisableDiscountCouponCommand()
            {
                DiscountCouponId = discountCoupon.Id,
            };

            _userService.UserId.Returns(userId);
            _discountCouponRepository.GetDiscountCouponById(command.DiscountCouponId)
                                     .Throws(new ForbidException("You are unable to proceed this action"));

            var act = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command, CancellationToken.None));

            await _discountCouponRepository.Received().GetDiscountCouponById(Arg.Any<DiscountCouponId>());
            await _unitOfWork.DidNotReceive().CommitChangesAsync();

            Assert.IsType<ForbidException>(act);
        }

        [Fact]
        public async Task DisableDiscountCoupon_ThrowsForbidException_IfDiscountCouponDoesntExist()
        {
            var userId = Guid.NewGuid();
            var command = new DisableDiscountCouponCommand()
            {
                DiscountCouponId = Guid.NewGuid(),
            };

            _userService.UserId.Returns(userId);
            _discountCouponRepository.GetDiscountCouponById(Guid.NewGuid())
                                     .ThrowsAsync(new ForbidException("Discount not found or you are unable to proceed this action"));

            var act = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command, CancellationToken.None));

            await _discountCouponRepository.Received().GetDiscountCouponById(Arg.Any<DiscountCouponId>());
            await _unitOfWork.DidNotReceive().CommitChangesAsync();

            Assert.IsType<ForbidException>(act);
        }
    }
}
