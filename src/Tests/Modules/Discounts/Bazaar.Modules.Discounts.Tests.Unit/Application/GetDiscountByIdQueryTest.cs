using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Application.Queries.Discounts.GetDiscountById;
using Modules.Discounts.Domain.Repositories;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Bazaar.Modules.Discounts.Tests.Unit.Application
{
    public class GetDiscountByIdQueryTest
    {
        private readonly GetDiscountByIdQueryHandler _sut;
        private readonly IDiscountRepository _discountRepository = Substitute.For<IDiscountRepository>();

        public GetDiscountByIdQueryTest()
        {
            _sut = new GetDiscountByIdQueryHandler(_discountRepository);
        }

        [Fact]
        public async Task GetDiscountById_ReturnsDiscount_IfExists()
        {
            var discountList = new DiscountListMock(Guid.NewGuid()).Discounts;
            var command = new GetDiscountByIdQuery()
            {
                Id = discountList[0].Id
            };

            _discountRepository.GetDiscountById(command.Id).Returns(discountList[0]);

            var result = await _sut.Handle(command, CancellationToken.None);

            await _discountRepository.Received().GetDiscountById(command.Id);

            Assert.IsType<DiscountDto>(result);
            Assert.Equal(command.Id, result.Id);
        }

        [Fact]
        public async Task GetDiscountById_ThrowsNotFoundException_IfDoesntExist()
        {
            var discountList = new DiscountListMock(Guid.NewGuid()).Discounts;
            var command = new GetDiscountByIdQuery()
            {
                Id = Guid.NewGuid()
            };

            _discountRepository.GetDiscountById(command.Id).ThrowsAsync(new NotFoundException("Discount not found."));

            var act = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));

            await _discountRepository.Received().GetDiscountById(command.Id);

            Assert.IsType<NotFoundException>(act);
        }
    }
}
