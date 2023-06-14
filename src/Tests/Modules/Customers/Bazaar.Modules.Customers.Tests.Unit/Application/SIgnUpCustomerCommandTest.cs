using Modules.Customers.Application.Commands.SignUpCustomerCommand;
using Modules.Customers.Domain.Repositories;
using Moq;
using Shared.Abstractions.Auth;
using Shared.Abstractions.UnitOfWork;

namespace Bazaar.Modules.Customers.Tests.Unit.Application
{
    public class SIgnUpCustomerCommandTest
    {
        private readonly SignUpCommandHandler _sut;
        private readonly Mock<ICustomerRepository> _customerRepository = new();
        private readonly Mock<ITokenManager> _tokenManager = new();
        private readonly Mock<IHashingService> _hashingService = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        public SIgnUpCustomerCommandTest()
        {
            _sut = new SignUpCommandHandler(_customerRepository.Object,
                                            _tokenManager.Object,
                                            _hashingService.Object,
                                            _unitOfWork.Object);
        }

        [Fact]
        public async Task SignUpCustomer_ValidParams_ShouldReturnTokenAndId()
        {

        }

        [Fact]
        public async Task SignUpCustomer_InValidParams_ShouldThrowException()
        {

        }
    }
}
