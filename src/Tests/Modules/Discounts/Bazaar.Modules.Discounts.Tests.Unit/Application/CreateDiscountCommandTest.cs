using Modules.Discounts.Application.Commands.Discounts.CreateDiscount;
using Modules.Discounts.Application.Contracts;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Exceptions;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;
using Modules.Products.Contracts.Interfaces;
using NSubstitute;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class CreateDiscountCommandTest
    {
        private readonly CreateDiscountCommandHandler _sut;
        private readonly IDiscountRepository _discountRepository = Substitute.For<IDiscountRepository>();
        private readonly IDiscountsUnitOfWork _unitOfWork = Substitute.For<IDiscountsUnitOfWork>();
        private readonly ICurrentUserService _userService = Substitute.For<ICurrentUserService>();
        private readonly IProductChecker _productChecker = Substitute.For<IProductChecker>();
        public CreateDiscountCommandTest()
        {
            _sut = new CreateDiscountCommandHandler(_userService, _discountRepository, _unitOfWork, _productChecker);
        }

        [Fact]
        public async Task AddDiscount_ReturnsGuid_IfAllParamsAreValid()
        {
            var shopId = Guid.NewGuid();
            var command = new CreateDiscountCommand()
            { 
                DiscountValue = 10,
                IsPercentageDiscount = true,
                Currency = String.Empty,
                DiscountTargetId = shopId,
                DiscountType = DiscountType.AssignedToVendors
            };

            _userService.UserId.Returns(shopId);
            _userService.UserRole.Returns("shop");

            var result = await _sut.Handle(command, CancellationToken.None);

            await _discountRepository.ReceivedWithAnyArgs().Add(Arg.Any<Discount>());
            await _unitOfWork.Received().CommitAndDispatchDomainEventsAsync(Arg.Any<Discount>());

            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task AddDiscount_ReturnsGuid_IfDiscountTargetParamsAreValid()
        {
            var shopId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var command = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = true,
                Currency = String.Empty,
                DiscountTargetId = customerId,
                DiscountType = DiscountType.AssignedToCustomer
            };

            _userService.UserId.Returns(shopId);
            _userService.UserRole.Returns("shop");

            var result = await _sut.Handle(command, CancellationToken.None);

            await _discountRepository.ReceivedWithAnyArgs().Add(Arg.Any<Discount>());
            await _unitOfWork.Received().CommitAndDispatchDomainEventsAsync(Arg.Any<Discount>());

            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task AddDiscount_ReturnsGuid_IfProductParamsAreValid()
        {
            var shopId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var command = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = false,
                Currency = "USD",
                DiscountTargetId = productId,
                DiscountType = DiscountType.AssignedToProduct
            };

            _userService.UserId.Returns(shopId);
            _userService.UserRole.Returns("shop");
            _productChecker.IsProductExisting(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(true);

            var result = await _sut.Handle(command, CancellationToken.None);

            await _discountRepository.ReceivedWithAnyArgs().Add(Arg.Any<Discount>());
            await _unitOfWork.Received().CommitAndDispatchDomainEventsAsync(Arg.Any<Discount>());

            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task AddDiscount_ReturnsGuid_IfAdminParamsAreValid()
        {
            var adminId = Guid.NewGuid();
            var command = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = false,
                Currency = "USD",
                DiscountTargetId = Guid.Empty,
                DiscountType = DiscountType.AssignedToAllProducts
            };

            _userService.UserId.Returns(adminId);
            _userService.UserRole.Returns("admin");

            var result = await _sut.Handle(command, CancellationToken.None);

            await _discountRepository.ReceivedWithAnyArgs().Add(Arg.Any<Discount>());
            await _unitOfWork.Received().CommitAndDispatchDomainEventsAsync(Arg.Any<Discount>());

            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task AddDiscount_ReturnsGuid_IfAdminDiscountTargetAllParamsAreValid()
        {
            var adminId = Guid.NewGuid();
            var command = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = false,
                Currency = "USD",
                DiscountTargetId = Guid.Empty,
                DiscountType = DiscountType.AssignedToOrderTotal
            };

            _userService.UserId.Returns(adminId);
            _userService.UserRole.Returns("admin");

            var result = await _sut.Handle(command, CancellationToken.None);

            await _discountRepository.ReceivedWithAnyArgs().Add(Arg.Any<Discount>());
            await _unitOfWork.Received().CommitAndDispatchDomainEventsAsync(Arg.Any<Discount>());

            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task AddDiscount_ReturnsGuid_IfAdminDiscountTargetParamsAreValid()
        {
            var adminId = Guid.NewGuid();
            var command = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = true,
                Currency = "USD",
                DiscountTargetId = Guid.Empty,
                DiscountType = DiscountType.AssignedToShipping
            };

            _userService.UserId.Returns(adminId);
            _userService.UserRole.Returns("admin");

            var result = await _sut.Handle(command, CancellationToken.None);

            await _discountRepository.ReceivedWithAnyArgs().Add(Arg.Any<Discount>());
            await _unitOfWork.Received().CommitAndDispatchDomainEventsAsync(Arg.Any<Discount>());

            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task AddDiscount_ThrowsForbidException_IfAdminDiscountTargetTypeParamsAreInvalid()
        {
            var adminId = Guid.NewGuid();
            var command1 = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = true,
                Currency = "USD",
                DiscountTargetId = Guid.NewGuid(),
                DiscountType = DiscountType.AssignedToCustomer
            };

            var command2 = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = true,
                Currency = "USD",
                DiscountTargetId = Guid.NewGuid(),
                DiscountType = DiscountType.AssignedToVendors
            };

            var command3 = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = true,
                Currency = "USD",
                DiscountTargetId = Guid.NewGuid(),
                DiscountType = DiscountType.AssignedToProduct
            };

            _userService.UserId.Returns(adminId);
            _userService.UserRole.Returns("admin");

            var act1 = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command1, CancellationToken.None));
            var act2 = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command2, CancellationToken.None));
            var act3 = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command3, CancellationToken.None));

            await _discountRepository.DidNotReceive().Add(Arg.Any<Discount>());
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<Discount>());

            Assert.IsType<ForbidException>(act1);
            Assert.IsType<ForbidException>(act2);
            Assert.IsType<ForbidException>(act3);
        }

        [Fact]
        public async Task AddDiscount_ThrowsForbidException_IfShopParamsAreInvalid()
        {
            var shopId = Guid.NewGuid();
            var command1 = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = false,
                Currency = "USD",
                DiscountTargetId = Guid.Empty,
                DiscountType = DiscountType.AssignedToShipping
            };

            var command2 = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = false,
                Currency = "USD",
                DiscountTargetId = Guid.Empty,
                DiscountType = DiscountType.AssignedToAllProducts
            };

            var command3 = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = true,
                Currency = String.Empty,
                DiscountTargetId = Guid.Empty,
                DiscountType = DiscountType.AssignedToProduct
            };

            _userService.UserId.Returns(shopId);
            _userService.UserRole.Returns("shop");

            var act1 = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command1, CancellationToken.None));
            var act2 = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command2, CancellationToken.None));
            var act3 = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command3, CancellationToken.None));

            await _discountRepository.DidNotReceive().Add(Arg.Any<Discount>());
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<Discount>());

            Assert.IsType<ForbidException>(act1);
            Assert.IsType<ForbidException>(act2);
            Assert.IsType<ForbidException>(act3);
        }

        [Fact]
        public async Task AddDiscount_ThrowsForbidException_IfCustomerTriesToCreateDiscount()
        {
            var customerId = Guid.NewGuid();
            var command1 = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = true,
                Currency = String.Empty,
                DiscountTargetId = Guid.Empty,
                DiscountType = DiscountType.AssignedToShipping
            };

            _userService.UserId.Returns(customerId);
            _userService.UserRole.Returns("customer");

            var act1 = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command1, CancellationToken.None));

            await _discountRepository.DidNotReceive().Add(Arg.Any<Discount>());
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<Discount>());

            Assert.IsType<ForbidException>(act1);
        }

        [Fact]
        public async Task AddDiscount_ThrowsForbidException_IfShopTriesToCreateDiscountToWrongProduct()
        {
            var shopId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var command1 = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = true,
                Currency = String.Empty,
                DiscountTargetId = productId,
                DiscountType = DiscountType.AssignedToProduct
            };

            _userService.UserId.Returns(shopId);
            _userService.UserRole.Returns("shop");
            _productChecker.IsProductExisting(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(false);

            var act1 = await Assert.ThrowsAsync<ForbidException>(() => _sut.Handle(command1, CancellationToken.None));

            await _discountRepository.DidNotReceive().Add(Arg.Any<Discount>());
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<Discount>());

            Assert.IsType<ForbidException>(act1);
        }

        [Fact]
        public async Task AddDiscount_ThrowsInvalidDiscountCurrencyException_IfValueCurrencyParamsAreInvalid()
        {
            var shopId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var command1 = new CreateDiscountCommand()
            {
                DiscountValue = 10,
                IsPercentageDiscount = false,
                Currency = String.Empty,
                DiscountTargetId = productId,
                DiscountType = DiscountType.AssignedToProduct
            };

            _userService.UserId.Returns(shopId);
            _userService.UserRole.Returns("shop");
            _productChecker.IsProductExisting(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(true);

            var act1 = await Assert.ThrowsAsync<InvalidDiscountCurrencyException>(() => _sut.Handle(command1, CancellationToken.None));

            await _discountRepository.DidNotReceive().Add(Arg.Any<Discount>());
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<Discount>());

            Assert.IsType<InvalidDiscountCurrencyException>(act1);
        }
    }
}
