using Bazaar.Modules.Discounts.Tests.Unit.Domain;
using Modules.Discounts.Application.Commands.DiscountCoupons.CreateDiscountCoupon;
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
    public class CreateDiscountCouponCommandTest
    {
        private readonly CreateDiscountCouponCommandHandler _sut;
        private readonly IDiscountRepository _discountRepository = Substitute.For<IDiscountRepository>();
        private readonly IDiscountsUnitOfWork _unitOfWork = Substitute.For<IDiscountsUnitOfWork>();
        private readonly IDiscountCouponRepository _discountCouponRepository = Substitute.For<IDiscountCouponRepository>();
        private readonly ICurrentUserService _userService = Substitute.For<ICurrentUserService>();
        public CreateDiscountCouponCommandTest()
        {
            _sut = new CreateDiscountCouponCommandHandler(_userService, _discountCouponRepository, _discountRepository, _unitOfWork);
        }

        [Fact]
        public async Task CreateDiscountCoupon_ReturnsGuid_AllParamsValid()
        {
            var discount = DiscountFactory.GetValueDiscount();
            var userId = discount.CreatedBy;
            var command = new CreateDiscountCouponCommand()
            {
                DiscountId = discount.Id,
                StartsAt = DateTimeOffset.UtcNow,
                ExpirationDate = DateTimeOffset.UtcNow.AddDays(1),
            };

            _userService.UserId.Returns(userId);
            _discountRepository.GetDiscountById(Arg.Any<DiscountId>()).Returns(discount);

            var result = await _sut.Handle(command, CancellationToken.None);

            await _discountRepository.Received().GetDiscountById(Arg.Any<DiscountId>());
            await _unitOfWork.Received().CommitAndDispatchDomainEventsAsync(Arg.Any<DiscountCoupon>());

            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task CreateDiscountCoupon_ThrowsForbidException_IfInvalidUser()
        {
            var userId = Guid.NewGuid();
            var discount = DiscountFactory.GetValueDiscount();
            var command = new CreateDiscountCouponCommand()
            {
                DiscountId = discount.Id,
                StartsAt = DateTimeOffset.UtcNow,
                ExpirationDate = DateTimeOffset.UtcNow.AddDays(1),
            };

            _userService.UserId.Returns(userId);
            _discountRepository.GetDiscountById(Arg.Any<DiscountId>()).Returns(discount);

            var act = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command, CancellationToken.None));

            await _discountRepository.Received().GetDiscountById(Arg.Any<DiscountId>());
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<DiscountCoupon>());

            Assert.IsType<ForbidException>(act);
        }

        [Fact]
        public async Task CreateDiscountCoupon_ThrowsForbidException_IfDiscountDoesntExist()
        {
            var userId = Guid.NewGuid();
            var command = new CreateDiscountCouponCommand()
            {
                DiscountId = Guid.NewGuid(),
                StartsAt = DateTimeOffset.UtcNow,
                ExpirationDate = DateTimeOffset.UtcNow.AddDays(1),
            };

            _userService.UserId.Returns(userId);
            _discountRepository.GetDiscountById(Guid.NewGuid()).Throws(new ForbidException("You are unable to proceed this action"));

            var act = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command, CancellationToken.None));

            await _discountRepository.Received().GetDiscountById(Arg.Any<DiscountId>());
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<DiscountCoupon>());

            Assert.IsType<ForbidException>(act);
        }

        [Fact]
        public async Task CreateDiscountCoupon_ThrowsForbidException_IfDatesAreInvalid()
        {
            var discount = DiscountFactory.GetValueDiscount();
            var userId = discount.CreatedBy;
            var command = new CreateDiscountCouponCommand()
            {
                DiscountId = discount.Id,
                StartsAt = DateTimeOffset.UtcNow.AddDays(1),
                ExpirationDate = DateTimeOffset.UtcNow,
            };

            _userService.UserId.Returns(userId);
            _discountRepository.GetDiscountById(discount.Id).Returns(discount);

            var act = await Assert.ThrowsAsync<InvalidDiscountCouponExpirationDateException>(() => _sut.Handle(command, CancellationToken.None));

            await _discountRepository.Received().GetDiscountById(Arg.Any<DiscountId>());
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<DiscountCoupon>());

            Assert.IsType<InvalidDiscountCouponExpirationDateException>(act);
        }
    }
}
