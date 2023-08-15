using Modules.Shippings.Application.Commands.ShippingMethods.ChangeShippingMethodAvailability;
using Modules.Shippings.Application.Contracts;
using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.Repositories;
using Modules.Shippings.Domain.ValueObjects;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Shared.Application.Exceptions;
using System.ComponentModel.Design;

namespace Bazaar.Modules.Shippings.Tests.Unit.ShippingMethods.Application
{
    public class ChangeShippingMethodAvailabilityCommandTest
    {
        private readonly ChangeShippingMethodAvailabilityCommandHandler _sut;
        private readonly IShippingMethodRepository _shippingMethodRepository = Substitute.For<IShippingMethodRepository>();
        private readonly IShippingMethodsUnitOfWork _unitOfWork = Substitute.For<IShippingMethodsUnitOfWork>();
        public ChangeShippingMethodAvailabilityCommandTest()
        {
            _sut = new ChangeShippingMethodAvailabilityCommandHandler(_shippingMethodRepository, _unitOfWork);
        }

        [Fact]
        public async Task ChangeShippingMethodAvailability_ChangesAvailability()
        {
            var shippingMethodList = new ShippingMethodListMock().ShippingMethods;
            
            var command = new ChangeShippingMethodAvailabilityCommand()
            {
                Id = shippingMethodList[0].Id
            };

            Assert.True(shippingMethodList[0].IsAvailable);

            _shippingMethodRepository.GetShippingMethodById(command.Id).Returns(shippingMethodList[0]);
            await _sut.Handle(command, CancellationToken.None);

            await _shippingMethodRepository.Received().GetShippingMethodById(command.Id);
            await _unitOfWork.Received().CommitAndDispatchDomainEventsAsync(Arg.Any<ShippingMethod>());

            Assert.False(shippingMethodList[0].IsAvailable);
        }

        [Fact]
        public async Task ChangeShippingMethodAvailability_ThrowsNotFoundIfIdIsInvalid()
        {
            var shippingMethodList = new ShippingMethodListMock();
            var command = new ChangeShippingMethodAvailabilityCommand()
            {
                Id = Guid.NewGuid()
            };

            var act = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));

            await _shippingMethodRepository.Received().GetShippingMethodById(command.Id);
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<ShippingMethod>());

            Assert.IsType<NotFoundException>(act);
        }
    }
}
