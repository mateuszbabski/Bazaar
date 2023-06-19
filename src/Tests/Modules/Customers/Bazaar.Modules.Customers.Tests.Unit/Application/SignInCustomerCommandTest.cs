using Bazaar.Modules.Customers.Tests.Unit.Domain;
using Modules.Customers.Application.Commands.SignInCustomer;
using Modules.Customers.Domain.Repositories;
using Moq;
using Shared.Abstractions.Auth;
using Shared.Application.Auth;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Customers.Tests.Unit.Application
{
    public class SignInCustomerCommandTest
    {
        private readonly SignInCustomerCommandHandler _sut;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly Mock<ITokenManager> _tokenManagerMock = new();
        private readonly Mock<IHashingService> _hashingServiceMock = new();

        public SignInCustomerCommandTest()
        {
            _sut = new SignInCustomerCommandHandler(_customerRepositoryMock.Object,
                                            _tokenManagerMock.Object,
                                            _hashingServiceMock.Object);
        }

        [Fact]
        public async Task SignInCustomer_ValidEmailAndPassword_RetursAuthenticationResult()
        {
            var customer = CustomerFactory.GetCustomer();

            var command = new SignInCustomerCommand()
            {
                Email = "customer@example.com",
                Password = "passwordHash"
            };

            _customerRepositoryMock.Setup(x => x.GetCustomerByEmail(It.IsAny<string>())).ReturnsAsync(customer);
            _hashingServiceMock.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _tokenManagerMock.Setup(x => x.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Roles>())).Returns("token");

            var result = await _sut.Handle(command, CancellationToken.None);

            Assert.IsType<AuthenticationResult>(result);
            Assert.Equal(customer.Id.Value, result.Id);
            Assert.Equal("token", result.Token);
        }

        [Fact]
        public async Task SignInCustomer_InValidEmail_ThrowsBadRequestException()
        {
            var command = new SignInCustomerCommand()
            {
                Email = "customer@example.com",
                Password = "password1"
            };

            _customerRepositoryMock.Setup(x => x.GetCustomerByEmail(It.IsAny<string>())).ThrowsAsync(new BadRequestException("Invalid email or password"));

            var act = await Assert.ThrowsAsync<BadRequestException>(() => _sut.Handle(command, CancellationToken.None));

            Assert.IsType<BadRequestException>(act);
            Assert.Equal("Invalid email or password", act.Message);
        }

        [Fact]
        public async Task SignInCustomer_InValidPassword_ThrowsBadRequestException()
        {
            var customer = CustomerFactory.GetCustomer();

            var command = new SignInCustomerCommand()
            {
                Email = "customer@example.com",
                Password = "password1"
            };

            _customerRepositoryMock.Setup(x => x.GetCustomerByEmail(It.IsAny<string>())).ReturnsAsync(customer);
            _hashingServiceMock.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var act = await Assert.ThrowsAsync<BadRequestException>(() => _sut.Handle(command, CancellationToken.None));

            Assert.IsType<BadRequestException>(act);
            Assert.Equal("Invalid email or password", act.Message);
        }
    }
}
