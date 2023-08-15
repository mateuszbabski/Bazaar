using Modules.Shippings.Application.Commands.ShippingMethods.AddShippingMethod;
using Modules.Shippings.Application.Commands.ShippingMethods.ChangeShippingMethodAvailability;
using Modules.Shippings.Application.Contracts;
using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.Exceptions;
using Modules.Shippings.Domain.Repositories;
using NSubstitute;
using Shared.Abstractions.UnitOfWork;
using Shared.Domain.Exceptions;

namespace Bazaar.Modules.Shippings.Tests.Unit.ShippingMethods.Application
{
    public class AddShippingMethodCommandTest
    {
        private readonly AddShippingMethodCommandHandler _sut;
        private readonly IShippingMethodRepository _shippingMethodRepository = Substitute.For<IShippingMethodRepository>();
        private readonly IShippingMethodsUnitOfWork _unitOfWork = Substitute.For<IShippingMethodsUnitOfWork>();
        public AddShippingMethodCommandTest()
        {
            _sut = new AddShippingMethodCommandHandler(_shippingMethodRepository, _unitOfWork);
        }

        [Fact]
        public async Task AddShippingMethod_ValidParams_ShouldReturnShippingMethodId()
        {
            var command = new AddShippingMethodCommand()
            {
                Name = "Name",
                Amount = 10,
                Currency = "PLN",
                DurationInDays = 1
            };
            
            var result = await _sut.Handle(command, CancellationToken.None);

            await _shippingMethodRepository.Received().AddShippingMethod(Arg.Any<ShippingMethod>());
            await _unitOfWork.Received().CommitAndDispatchDomainEventsAsync(Arg.Any<ShippingMethod>());

            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task AddShippingMethodInvalidDurationTime_ShouldThrowInvalidDurationTimeException()
        {
            var command = new AddShippingMethodCommand()
            {
                Name = "Name",
                Amount = 10,
                Currency = "PLN",
                DurationInDays = 0
            };

            var act = await Assert.ThrowsAsync<InvalidDurationTimeException>(() => _sut.Handle(command, CancellationToken.None));

            await _shippingMethodRepository.DidNotReceive().AddShippingMethod(Arg.Any<ShippingMethod>());
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<ShippingMethod>());

            Assert.IsType<InvalidDurationTimeException>(act);
        }

        [Fact]
        public async Task AddShippingMethod_InvalidPrice_ShouldThrowInvalidPriceException()
        {
            var command = new AddShippingMethodCommand()
            {
                Name = "Name",
                Amount = 0,
                Currency = "PLN",
                DurationInDays = 1
            };
            var act = await Assert.ThrowsAsync<InvalidPriceException>(() => _sut.Handle(command, CancellationToken.None));

            await _shippingMethodRepository.DidNotReceive().AddShippingMethod(Arg.Any<ShippingMethod>());
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<ShippingMethod>());

            Assert.IsType<InvalidPriceException>(act);
        }
    }
}
