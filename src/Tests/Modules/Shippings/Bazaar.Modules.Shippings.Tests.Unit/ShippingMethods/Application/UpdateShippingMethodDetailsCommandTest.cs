using Modules.Shippings.Application.Commands.ShippingMethods.ChangeShippingMethodAvailability;
using Modules.Shippings.Application.Commands.ShippingMethods.UpdateShippingMethodDetails;
using Modules.Shippings.Application.Contracts;
using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.Repositories;
using NSubstitute;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Shippings.Tests.Unit.ShippingMethods.Application
{
    public class UpdateShippingMethodDetailsCommandTest
    {
        private readonly UpdateShippingMethodDetailsCommandHandler _sut;
        private readonly IShippingMethodRepository _shippingMethodRepository = Substitute.For<IShippingMethodRepository>();
        private readonly IShippingMethodsUnitOfWork _unitOfWork = Substitute.For<IShippingMethodsUnitOfWork>();
        public UpdateShippingMethodDetailsCommandTest()
        {
            _sut = new UpdateShippingMethodDetailsCommandHandler(_shippingMethodRepository, _unitOfWork);
        }

        [Fact]
        public async Task UpdateShippingMethodDetails_ChangesAllFields()
        {
            var shippingMethodList = new ShippingMethodListMock().ShippingMethods;

            var command = new UpdateShippingMethodDetailsCommand()
            {
                Id = shippingMethodList[0].Id,
                Name = "updatedName",
                Amount = 20,
                Currency = "USD",
                DurationTime = 5
            };

            _shippingMethodRepository.GetShippingMethodById(command.Id).Returns(shippingMethodList[0]);
            await _sut.Handle(command, CancellationToken.None);

            await _shippingMethodRepository.Received().GetShippingMethodById(command.Id);
            await _unitOfWork.Received().CommitAndDispatchDomainEventsAsync(Arg.Any<ShippingMethod>());

            Assert.Equal("updatedName", shippingMethodList[0].Name);
            Assert.Equal(20, shippingMethodList[0].BasePrice.Amount);
            Assert.Equal("USD", shippingMethodList[0].BasePrice.Currency);
            Assert.Equal(5, shippingMethodList[0].DurationTimeInDays);
        }

        [Fact]
        public async Task UpdateShippingMethodDetails_ChangesOnlyValidFields()
        {
            var shippingMethodList = new ShippingMethodListMock().ShippingMethods;
            var command = new UpdateShippingMethodDetailsCommand()
            {
                Id = shippingMethodList[0].Id,
                Name = "updatedName",
                Amount = 0,
                Currency = "PLN",
                DurationTime = 0
            };

            _shippingMethodRepository.GetShippingMethodById(command.Id).Returns(shippingMethodList[0]);
            await _sut.Handle(command, CancellationToken.None);

            await _shippingMethodRepository.Received().GetShippingMethodById(command.Id);
            await _unitOfWork.Received().CommitAndDispatchDomainEventsAsync(Arg.Any<ShippingMethod>());

            Assert.Equal("updatedName", shippingMethodList[0].Name);
            Assert.Equal(10, shippingMethodList[0].BasePrice.Amount);
            Assert.Equal("PLN", shippingMethodList[0].BasePrice.Currency);
            Assert.Equal(1, shippingMethodList[0].DurationTimeInDays);
        }

        [Fact]
        public async Task UpdateShippingMethodDetails_ThrowsNotFoundIfIdIsInvalid()
        {
            var shippingMethodList = new ShippingMethodListMock().ShippingMethods;
            var command = new UpdateShippingMethodDetailsCommand()
            {
                Id = Guid.NewGuid(),
                Name = "updatedName",
                DurationTime = 5
            };

            var act = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));

            await _shippingMethodRepository.Received().GetShippingMethodById(command.Id);
            await _unitOfWork.DidNotReceive().CommitAndDispatchDomainEventsAsync(Arg.Any<ShippingMethod>());

            Assert.IsType<NotFoundException>(act);
        }
    }
}
