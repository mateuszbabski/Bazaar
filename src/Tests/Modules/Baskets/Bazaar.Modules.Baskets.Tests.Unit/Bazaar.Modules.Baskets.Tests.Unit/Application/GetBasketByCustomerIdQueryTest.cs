﻿using Bazaar.Modules.Baskets.Tests.Unit.Domain;
using Modules.Baskets.Application.Dtos;
using Modules.Baskets.Application.Queries.GetBasketByCustomerId;
using Modules.Baskets.Domain.Entities;
using Modules.Baskets.Domain.Repositories;
using Moq;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Baskets.Tests.Unit.Application
{
    public class GetBasketByCustomerIdQueryTest
    {
        private readonly GetBasketByCustomerIdCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _userService = new();
        private readonly Mock<IBasketRepository> _basketRepository = new();
        public GetBasketByCustomerIdQueryTest()
        {
            _sut = new GetBasketByCustomerIdCommandHandler(_userService.Object,
                                                                 _basketRepository.Object);
        }

        [Fact]
        public async void GetBasket_ReturnsCart_IfCustomerHasOne()
        {
            var customerId = Guid.NewGuid();
            var basket = CreateBasketWithProduct(customerId);

            _userService.Setup(x => x.UserId).Returns(customerId);
            _basketRepository.Setup(x => x.GetBasketByCustomerId(customerId)).ReturnsAsync(basket);

            var result = await _sut.Handle(new GetBasketByCustomerIdCommand(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<BasketDto>(result);
            Assert.Equal(customerId, basket.CustomerId.Value);
        }

        [Fact]
        public async void GetBasket_ThrowsException_IfCustomerDoesntHaveAny()
        {
            var customerId = Guid.NewGuid();

            _userService.Setup(x => x.UserId).Returns(customerId);
            _basketRepository.Setup(x => x.GetBasketByCustomerId(customerId))
                .ThrowsAsync(new NotFoundException("Basket not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(new GetBasketByCustomerIdCommand(),
                                                                                      CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Basket not found.", result.Message);
        }
        private static Basket CreateBasketWithProduct(Guid customerId)
        {
            var basket = Basket.CreateBasket(customerId, "USD");
            var product = BasketFactory.GetProduct();

            basket.AddProductToBasket(product.Id, product.ShopId, 1, product.Price, product.Price.Amount);

            return basket;
        }
    }
}
