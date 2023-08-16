using Modules.Shippings.Application.Dtos;
using Modules.Shippings.Application.Queries.ShippingMethods.GetShippingMethodById;
using Modules.Shippings.Domain.Repositories;
using NSubstitute;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Shippings.Tests.Unit.ShippingMethods.Application
{
    public class GetShippingMethodByIdQueryTest
    {
        private readonly GetShippingMethodByIdQueryHandler _sut;
        private readonly IShippingMethodRepository _shippingMethodRepository = Substitute.For<IShippingMethodRepository>();
        public GetShippingMethodByIdQueryTest()
        {
            _sut = new GetShippingMethodByIdQueryHandler(_shippingMethodRepository);
        }
        [Fact]
        public async Task GetShippingMethodById_ValidId_ReturnsShippingMethodDto()
        {
            var shippingMethodList = new ShippingMethodListMock().ShippingMethods;

            var query = new GetShippingMethodByIdQuery()
            {
                Id = shippingMethodList[0].Id
            };

            _shippingMethodRepository.GetShippingMethodById(query.Id).Returns(shippingMethodList[0]);
            var result = await _sut.Handle(query, CancellationToken.None);

            await _shippingMethodRepository.Received().GetShippingMethodById(query.Id);

            Assert.IsType<ShippingMethodDto>(result);
            Assert.True(shippingMethodList[0].Id.Value == result.Id);
        }

        [Fact]
        public async Task GetShippingMethodById_InvalidId_ThrowsNotFoundException()
        {
            var shippingMethodList = new ShippingMethodListMock().ShippingMethods;

            var query = new GetShippingMethodByIdQuery()
            {
                Id = Guid.NewGuid()
            };

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(query, CancellationToken.None));

            await _shippingMethodRepository.Received().GetShippingMethodById(query.Id);

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Shipping method not found", result.Message);
        }
    }
}
