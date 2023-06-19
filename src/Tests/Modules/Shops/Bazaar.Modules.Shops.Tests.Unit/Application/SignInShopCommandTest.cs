using Bazaar.Modules.Shops.Tests.Unit.Domain;
using Modules.Shops.Application.Commands.SignInShop;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Abstractions.Auth;
using Shared.Application.Auth;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;

namespace Bazaar.Modules.Shops.Tests.Unit.Application
{
    public class SignInShopCommandTest
    {
        private readonly SignInShopCommandHandler _sut;
        private readonly Mock<IShopRepository> _shopRepositoryMock = new();
        private readonly Mock<ITokenManager> _tokenManagerMock = new();
        private readonly Mock<IHashingService> _hashingServiceMock = new();
        public SignInShopCommandTest()
        {
            _sut = new SignInShopCommandHandler(_shopRepositoryMock.Object,
                                                _tokenManagerMock.Object,
                                                _hashingServiceMock.Object);
        }

        [Fact]
        public async Task SignInShop_ValidEmailAndPassword_RetursAuthenticationResult()
        {
            var shop = ShopFactory.GetShop();

            var command = new SignInShopCommand()
            {
                Email = "shop@example.com",
                Password = "passwordHash"
            };

            _shopRepositoryMock.Setup(x => x.GetShopByEmail(It.IsAny<string>())).ReturnsAsync(shop);
            _hashingServiceMock.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _tokenManagerMock.Setup(x => x.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<Roles>())).Returns("token");

            var result = await _sut.Handle(command, CancellationToken.None);

            Assert.IsType<AuthenticationResult>(result);
            Assert.Equal(shop.Id.Value, result.Id);
            Assert.Equal("token", result.Token);
        }

        [Fact]
        public async Task SignInShop_InValidEmail_ThrowsBadRequestException()
        {
            var command = new SignInShopCommand()
            {
                Email = "shop@example.com",
                Password = "password1"
            };

            _shopRepositoryMock.Setup(x => x.GetShopByEmail(It.IsAny<string>())).ThrowsAsync(new BadRequestException("Invalid email or password"));

            var act = await Assert.ThrowsAsync<BadRequestException>(() => _sut.Handle(command, CancellationToken.None));

            Assert.IsType<BadRequestException>(act);
            Assert.Equal("Invalid email or password", act.Message);
        }

        [Fact]
        public async Task SignInShop_InValidPassword_ThrowsBadRequestException()
        {
            var shop = ShopFactory.GetShop();

            var command = new SignInShopCommand()
            {
                Email = "shop@example.com",
                Password = "password1"
            };

            _shopRepositoryMock.Setup(x => x.GetShopByEmail(It.IsAny<string>())).ReturnsAsync(shop);
            _hashingServiceMock.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var act = await Assert.ThrowsAsync<BadRequestException>(() => _sut.Handle(command, CancellationToken.None));

            Assert.IsType<BadRequestException>(act);
            Assert.Equal("Invalid email or password", act.Message);
        }
    }
}
