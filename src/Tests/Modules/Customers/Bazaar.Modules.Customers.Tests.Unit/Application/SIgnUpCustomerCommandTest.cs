﻿using Modules.Customers.Application.Commands.SignUpCustomer;
using Modules.Customers.Application.Contracts;
using Modules.Customers.Domain.Entities;
using Modules.Customers.Domain.Repositories;
using Moq;
using Shared.Abstractions.Auth;
using Shared.Application.Auth;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Customers.Tests.Unit.Application
{
    public class SIgnUpCustomerCommandTest
    {
        private readonly SignUpCustomerCommandHandler _sut;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly Mock<ITokenManager> _tokenManagerMock = new();
        private readonly Mock<IHashingService> _hashingServiceMock = new();
        private readonly Mock<ICustomersUnitOfWork> _unitOfWorkMock = new();

        public SIgnUpCustomerCommandTest()
        {
            _sut = new SignUpCustomerCommandHandler(_customerRepositoryMock.Object,
                                            _tokenManagerMock.Object,
                                            _hashingServiceMock.Object,
                                            _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task SignUpCustomer_ValidParams_ShouldReturnTokenAndId()
        {
            var command = new SignUpCustomerCommand()
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
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Customer>()), Times.Once);

            Assert.IsType<AuthenticationResult>(result);
            Assert.IsType<Guid>(result.Id);
        }

        [Fact]
        public async Task SignUpCustomer_InValidParams_ShouldThrowException()
        {
            var command = new SignUpCustomerCommand()
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
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Customer>()), Times.Never);

            Assert.IsType<FluentValidation.ValidationException>(act);
        }
    }
}
