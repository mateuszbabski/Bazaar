﻿using Bazaar.Modules.Shops.Tests.Unit.Domain;
using Modules.Shops.Application.Commands.UpdateShopDetails;
using Modules.Shops.Application.Contracts;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.Repositories;
using Moq;
using Shared.Abstractions.UserServices;

namespace Bazaar.Modules.Shops.Tests.Unit.Application
{
    public class UpdateShopCommandTest
    {
        private readonly UpdateShopDetailsCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _userServiceMock = new();
        private readonly Mock<IShopRepository> _shopRepositoryMock = new();
        private readonly Mock<IShopsUnitOfWork> _unitOfWorkMock = new();

        public UpdateShopCommandTest()
        {
            _sut = new UpdateShopDetailsCommandHandler(_userServiceMock.Object,
                                                       _shopRepositoryMock.Object,
                                                       _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task UpdateShopDetails_ValidField_ChangesFields()
        {
            var shop = ShopFactory.GetShop();

            var command = new UpdateShopDetailsCommand()
            {
                Id = shop.Id,
                ShopName = "New Name"
            };

            _userServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepositoryMock.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            await _sut.Handle(command, CancellationToken.None);

            Assert.Equal(shop.ShopName, command.ShopName);

            _unitOfWorkMock.Verify(x => x.CommitAndDispatchDomainEventsAsync(It.IsAny<Shop>()), Times.Once);
        }

        [Fact]
        public async Task UpdateShopDetails_EmptyFields_EntityNotChanged()
        {
            var shop = ShopFactory.GetShop();

            var command = new UpdateShopDetailsCommand()
            {
                Id = shop.Id
            };

            _userServiceMock.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepositoryMock.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            await _sut.Handle(command, CancellationToken.None);

            Assert.Equal("shopName", shop.ShopName);
        }
    }
}
