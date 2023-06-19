using Modules.Shops.Application.Commands.SignUpShop;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Abstractions.Auth;
using Shared.Abstractions.UnitOfWork;
using Shared.Application.Auth;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Shops.Tests.Unit.Application
{
    public class SignUpShopCommandTest
    {
        private readonly SignUpShopCommandHandler _sut;
        private readonly Mock<IShopRepository> _shopRepositoryMock = new();
        private readonly Mock<ITokenManager> _tokenManagerMock = new();
        private readonly Mock<IHashingService> _hashingServiceMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

        public SignUpShopCommandTest()
        {
            _sut = new SignUpShopCommandHandler(_shopRepositoryMock.Object,
                                                _tokenManagerMock.Object,
                                                _hashingServiceMock.Object,
                                                _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task SignUpShop_ValidParams_ShouldReturnTokenAndId()
        {
            var command = new SignUpShopCommand()
            {
                Email = "shop@example.com",
                Password = "password1",
                OwnerName = "OwnerName",
                OwnerLastName = "OwnerLastname",
                ContactNumber = "123456789",
                ShopName = "ShopName",
                TaxNumber = "123456",
                Country = "Poland",
                City = "Warsaw",
                Street = "Chmielna",
                PostalCode = "00-210"
            };

            _hashingServiceMock.Setup(x => x.GenerateHashPassword(It.IsAny<string>())).Returns("passwordHash");
            _tokenManagerMock.Setup(x => x.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Roles>())).Returns(It.IsAny<string>());

            var result = await _sut.Handle(command, CancellationToken.None);

            _shopRepositoryMock.Verify(x => x.Add(It.Is<Shop>(m => m.Id.Value == result.Id)), Times.Once);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchEventsAsync(), Times.Once);

            Assert.IsType<AuthenticationResult>(result);
            Assert.IsType<Guid>(result.Id);
        }

        [Fact]
        public async Task SignUpShopr_InValidParams_ShouldThrowException()
        {
            var command = new SignUpShopCommand()
            {
                Email = "",
                Password = "password1",
                OwnerName = "Shop",
                OwnerLastName = "Shop",
                ShopName = "ShopName",
                ContactNumber = "123456789",
                TaxNumber = "123456",
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

            _shopRepositoryMock.Verify(x => x.Add(It.IsAny<Shop>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.CommitAndDispatchEventsAsync(), Times.Never);

            Assert.IsType<FluentValidation.ValidationException>(act);
        }
    }
}
