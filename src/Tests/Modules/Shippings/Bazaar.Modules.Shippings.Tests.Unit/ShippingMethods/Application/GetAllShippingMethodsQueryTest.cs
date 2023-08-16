using Modules.Shippings.Application.Dtos;
using Modules.Shippings.Application.Queries.ShippingMethods.GetShippingMethods;
using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.Repositories;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Shippings.Tests.Unit.ShippingMethods.Application
{
    public class GetAllShippingMethodsQueryTest
    {
        private readonly GetAllShippingMethodsQueryHandler _sut;
        private readonly IShippingMethodRepository _shippingMethodRepository = Substitute.For<IShippingMethodRepository>();
        private readonly IQueryProcessor<ShippingMethod> _queryProcessor = Substitute.For<IQueryProcessor<ShippingMethod>>();
        public GetAllShippingMethodsQueryTest()
        {
            _sut = new GetAllShippingMethodsQueryHandler(_shippingMethodRepository, _queryProcessor);
        }
        // TODO: Check why test fails even Moq test works and manual testing works
        [Fact]
        public async Task GetAllShippingMethods_ValidQuery_ReturnsListShippingMethodDto()
        {
            var shippingMethodList = new ShippingMethodListMock().ShippingMethods;

            _shippingMethodRepository.GetShippingMethods().Returns(Task.FromResult(shippingMethodList.AsEnumerable()));

            _queryProcessor.SortQuery(Arg.Any<IQueryable<ShippingMethod>>(), Arg.Any<string>(), Arg.Any<string>())
                               .Returns(shippingMethodList.AsQueryable());

            _queryProcessor.PageQuery(Arg.Any<IEnumerable<ShippingMethod>>(), Arg.Any<int>(), Arg.Any<int>())
                               .Returns(shippingMethodList);

            var result = await _sut.Handle(new GetAllShippingMethodsQuery(), CancellationToken.None);

            await _shippingMethodRepository.Received().GetShippingMethods();

            _queryProcessor.Received()
                           .SortQuery(Arg.Any<IQueryable<ShippingMethod>>(), Arg.Any<string>(), Arg.Any<string>());

            _queryProcessor.Received()
                           .PageQuery(Arg.Any<IEnumerable<ShippingMethod>>(), Arg.Any<int>(), Arg.Any<int>());

            Assert.IsAssignableFrom<PagedList<ShippingMethodDto>>(result);
        }

        [Fact]
        public async Task GetAllShippingMethods_EmptyList_ThrowsNotFoundException()
        {
            _shippingMethodRepository.GetShippingMethods().ThrowsAsync(new NotFoundException("Shipping methods not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(new GetAllShippingMethodsQuery(), CancellationToken.None));

            await _shippingMethodRepository.Received().GetShippingMethods();

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Shipping methods not found.", result.Message);
        }
    }
}
