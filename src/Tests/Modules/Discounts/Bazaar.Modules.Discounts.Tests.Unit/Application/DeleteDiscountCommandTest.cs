using Modules.Discounts.Application.Commands.Discounts.DeleteDiscount;
using Modules.Discounts.Application.Contracts;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class DeleteDiscountCommandTest
    {
        private readonly DeleteDiscountCommandHandler _sut;
        private readonly ICurrentUserService _currentUserService = Substitute.For<ICurrentUserService>();
        private readonly IDiscountsUnitOfWork _unitOfWork = Substitute.For<IDiscountsUnitOfWork>();
        private readonly IDiscountRepository _discountRepository = Substitute.For<IDiscountRepository>();

        public DeleteDiscountCommandTest()
        {
            _sut = new DeleteDiscountCommandHandler(_currentUserService, _discountRepository, _unitOfWork);
        }

        [Fact]
        public async Task DeleteDiscount_ReturnsNoContent_IfDiscountCreatorIdMatchesUser()
        {
            var userId = Guid.NewGuid();
            var discount = GetValueDiscount(userId);
            var command = new DeleteDiscountCommand()
            {
                DiscountId = Guid.NewGuid(),
            };

            _currentUserService.UserId.Returns(userId);
            _discountRepository.GetDiscountById(command.DiscountId).Returns(discount);

            var result = await _sut.Handle(command, CancellationToken.None);

            _discountRepository.Received().Delete(discount);
            await _unitOfWork.Received().CommitChangesAsync();
            Assert.Equal(discount.CreatedBy, userId);
        }

        [Fact]
        public async Task DeleteDiscount_ThrowsForbidExceptiont_IfDiscountCreatorIdDoesntMatchUser()
        {
            var userId = Guid.NewGuid();
            var discount = GetValueDiscount(userId);
            var command = new DeleteDiscountCommand()
            {
                DiscountId = Guid.NewGuid(),
            };

            _currentUserService.UserId.Returns(Guid.NewGuid());
            _discountRepository.GetDiscountById(command.DiscountId).Returns(discount);

            var act = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command, CancellationToken.None));

            _discountRepository.DidNotReceive().Delete(discount);
            await _unitOfWork.DidNotReceive().CommitChangesAsync();
            Assert.IsType<ForbidException>(act);
        }

        [Fact]
        public async Task DeleteDiscount_ThrowsNotFoundException_IfDiscountNotFound()
        {
            var userId = Guid.NewGuid();
            var discount = GetValueDiscount(userId);
            var command = new DeleteDiscountCommand()
            {
                DiscountId = Guid.NewGuid(),
            };

            _currentUserService.UserId.Returns(userId);
            var act = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));

            _discountRepository.DidNotReceive().Delete(discount);
            await _unitOfWork.DidNotReceive().CommitChangesAsync();
            Assert.IsType<NotFoundException>(act);
        }

        public static Discount GetValueDiscount(Guid creatorId)
        {
            var discountTarget = DiscountTarget.CreateDiscountTarget(DiscountType.AssignedToVendors, creatorId);

            var discount = Discount.CreateValueDiscount(creatorId, 10, "USD", discountTarget);

            return discount;
        }
    }
}
