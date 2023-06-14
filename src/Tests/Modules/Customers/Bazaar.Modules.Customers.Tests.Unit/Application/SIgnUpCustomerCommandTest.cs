using Bazaar.Modules.Customers.Tests.Unit.Domain;
using FluentValidation;
using Modules.Customers.Application.Commands.SignUpCustomerCommand;
using Modules.Customers.Domain.Entities;
using Modules.Customers.Domain.Repositories;
using Moq;
using Shared.Abstractions.Auth;
using Shared.Abstractions.UnitOfWork;
using Shared.Application.Auth;
using Shared.Domain.Exceptions;
using Shared.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Bazaar.Modules.Customers.Tests.Unit.Application
{
    public class SIgnUpCustomerCommandTest
    {
        private readonly SignUpCommandHandler _sut;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly Mock<ITokenManager> _tokenManagerMock = new();
        private readonly Mock<IHashingService> _hashingServiceMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<SignUpValidator> _validatorMock = new();

        public SIgnUpCustomerCommandTest()
        {
            _sut = new SignUpCommandHandler(_customerRepositoryMock.Object,
                                            _tokenManagerMock.Object,
                                            _hashingServiceMock.Object,
                                            _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task SignUpCustomer_ValidParams_ShouldReturnTokenAndId()
        {
            var command = new SignUpCommand()
            {
                Email = "customer@example.com",
                Password = "password1",
                Name = "Customer",
                LastName = "Customer",
                TelephoneNumber = "123456789",
                Country = "Poland",
                City = "Warsaw",
                Street = "Chmielna",
                PostalCode = "00-210"
             };

            _hashingServiceMock.Setup(x => x.GenerateHashPassword(It.IsAny<string>())).Returns("passwordHash");
            _tokenManagerMock.Setup(x => x.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Roles>())).Returns(It.IsAny<string>());

            var result = await _sut.Handle(command, CancellationToken.None);

            _customerRepositoryMock.Verify(x => x.Add(It.Is<Customer>(m => m.Id.Value == result.Id)), Times.Once);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchEventsAsync(), Times.Once);

            Assert.IsType<AuthenticationResult>(result);
            Assert.IsType<Guid>(result.Id);
        }

        [Fact]
        public async Task SignUpCustomer_InValidParams_ShouldThrowException()
        {
            var command = new SignUpCommand()
            {
                Email = "",
                Password = "password1",
                Name = "Customer",
                LastName = "Customer",
                TelephoneNumber = "123456789",
                Country = "Poland",
                City = "Warsaw",
                Street = "Chmielna",
                PostalCode = "00-210"
            };

            _hashingServiceMock.Setup(x => x.GenerateHashPassword(It.IsAny<string>())).Returns("passwordHash");
            _tokenManagerMock.Setup(x => x.GenerateToken(It.IsAny<Guid>(),
                                                         It.IsAny<string>(),
                                                         It.IsAny<Roles>())).Returns(It.IsAny<string>());

            var act = await Assert.ThrowsAsync<FluentValidation.ValidationException>(() 
                => _sut.Handle(command, CancellationToken.None));

            _customerRepositoryMock.Verify(x => x.Add(It.IsAny<Customer>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchEventsAsync(), Times.Never);

            Assert.IsType<FluentValidation.ValidationException>(act);
        }
    }
}
