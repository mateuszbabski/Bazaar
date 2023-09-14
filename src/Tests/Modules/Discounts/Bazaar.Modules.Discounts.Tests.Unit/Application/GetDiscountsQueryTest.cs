using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Application.Queries.Discounts.GetDiscounts;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shared.Abstractions.Queries;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class GetDiscountsQueryTest
    {
        private readonly GetDiscountsQueryHandler _sut;
        private readonly ICurrentUserService _currentUserService = Substitute.For<ICurrentUserService>();
        private readonly IDiscountRepository _discountRepository = Substitute.For<IDiscountRepository>();
        private readonly IQueryProcessor<Discount> _queryProcessor = Substitute.For<IQueryProcessor<Discount>>();

        public GetDiscountsQueryTest()
        {
            _sut = new GetDiscountsQueryHandler(_currentUserService, _discountRepository, _queryProcessor);
        }

        [Fact]
        public async Task GetDiscounts_ReturnsDiscountList_IfDiscountsExist()
        {
            var userId = Guid.NewGuid();
            var discountList = new DiscountListMock(userId).Discounts;

            _discountRepository.GetAllCreatorDiscounts(userId).Returns(Task.FromResult(discountList.AsEnumerable()));

            _queryProcessor.SortQuery(Arg.Any<IQueryable<Discount>>(), Arg.Any<string>(), Arg.Any<string>())
                               .Returns(discountList.AsQueryable());

            _queryProcessor.PageQuery(Arg.Any<IEnumerable<Discount>>(), Arg.Any<int>(), Arg.Any<int>())
                               .Returns(discountList);

            var result = await _sut.Handle(new GetDiscountsQuery(), CancellationToken.None);

            await _discountRepository.Received().GetAllCreatorDiscounts(Arg.Any<Guid>());

            _queryProcessor.Received()
                           .SortQuery(Arg.Any<IQueryable<Discount>>(), Arg.Any<string>(), Arg.Any<string>());

            _queryProcessor.Received()
                           .PageQuery(Arg.Any<IEnumerable<Discount>>(), Arg.Any<int>(), Arg.Any<int>());

            Assert.IsAssignableFrom<PagedList<DiscountDto>>(result);
        }

        [Fact]
        public async Task GetDiscounts_ThrowsNotFound_IfDiscountListDoesntExist()
        {
            var userId = Guid.NewGuid();
            _currentUserService.UserId.Returns(userId);

            _discountRepository.GetAllCreatorDiscounts(userId).ThrowsAsync(new NotFoundException("Discounts not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(()
                => _sut.Handle(new GetDiscountsQuery(), CancellationToken.None));

            await _discountRepository.Received().GetAllCreatorDiscounts(Arg.Any<Guid>());

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Discounts not found.", result.Message);
        }
    }
}
