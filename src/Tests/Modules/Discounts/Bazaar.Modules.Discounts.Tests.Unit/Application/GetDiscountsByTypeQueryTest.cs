using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Application.Queries.Discounts.GetDiscountsByType;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class GetDiscountsByTypeQueryTest
    {
        private readonly GetDiscountsByTypeQueryHandler _sut;
        private readonly IDiscountRepository _discountRepository = Substitute.For<IDiscountRepository>();
        private readonly IQueryProcessor<Discount> _queryProcessor = Substitute.For<IQueryProcessor<Discount>>();
        public GetDiscountsByTypeQueryTest()
        {
            _sut = new GetDiscountsByTypeQueryHandler(_discountRepository, _queryProcessor);
        }

        [Fact]
        public async Task GetDiscountByType_ReturnsDiscounts_IfParamsAreValid()
        {
            var shopId = Guid.NewGuid();
            var discountList = new DiscountListMock(shopId).Discounts;

            var command = new GetDiscountsByTypeQuery()
            {
                DiscountType = DiscountType.AssignedToVendors,
                DiscountTargetId = shopId
            };

            _discountRepository.GetDiscountsByType(command.DiscountType, command.DiscountTargetId).Returns(Task.FromResult(discountList.AsEnumerable()));

            _queryProcessor.SortQuery(Arg.Any<IQueryable<Discount>>(), Arg.Any<string>(), Arg.Any<string>())
                               .Returns(discountList.AsQueryable());

            _queryProcessor.PageQuery(Arg.Any<IEnumerable<Discount>>(), Arg.Any<int>(), Arg.Any<int>())
                               .Returns(discountList);

            var result = await _sut.Handle(new GetDiscountsByTypeQuery(), CancellationToken.None);

            await _discountRepository.ReceivedWithAnyArgs().GetDiscountsByType(default, default);

            _queryProcessor.Received()
                           .SortQuery(Arg.Any<IQueryable<Discount>>(), Arg.Any<string>(), Arg.Any<string>());

            _queryProcessor.Received()
                           .PageQuery(Arg.Any<IEnumerable<Discount>>(), Arg.Any<int>(), Arg.Any<int>());

            Assert.IsAssignableFrom<PagedList<DiscountDto>>(result);
            Assert.Equal(result.Items.First().DiscountType.ToString(), DiscountType.AssignedToVendors.ToString());
            Assert.Equal(result.Items.First().DiscountTarget, shopId);
        }

        [Fact]
        public async Task GetDiscountByType_ReturnsNotFoundException_IfParamsAreInvalid()
        {
            var command = new GetDiscountsByTypeQuery()
            {
                DiscountType = DiscountType.AssignedToAllProducts,
                DiscountTargetId = Guid.Empty
            };

            _discountRepository.GetDiscountsByType(command.DiscountType, command.DiscountTargetId).ThrowsAsync(new NotFoundException("Discounts not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(command, CancellationToken.None));

            await _discountRepository.Received().GetDiscountsByType(Arg.Any<DiscountType>(), Arg.Any<Guid>());

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Discounts not found.", result.Message);
        }
    }
}
