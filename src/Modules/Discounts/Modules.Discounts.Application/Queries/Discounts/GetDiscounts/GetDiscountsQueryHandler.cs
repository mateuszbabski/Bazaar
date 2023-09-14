using MediatR;
using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Shared.Abstractions.Queries;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Modules.Discounts.Application.Queries.Discounts.GetDiscounts
{
    public class GetDiscountsQueryHandler : IRequestHandler<GetDiscountsQuery, PagedList<DiscountDto>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDiscountRepository _discountRepository;
        private readonly IQueryProcessor<Discount> _queryProcessor;

        public GetDiscountsQueryHandler(ICurrentUserService currentUserService,
                                        IDiscountRepository discountRepository,
                                        IQueryProcessor<Discount> queryProcessor)
        {
            _currentUserService = currentUserService;
            _discountRepository = discountRepository;
            _queryProcessor = queryProcessor;
        }
        public async Task<PagedList<DiscountDto>> Handle(GetDiscountsQuery query, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var baseQuery = await _discountRepository.GetAllCreatorDiscounts(userId)
                ?? throw new NotFoundException("Discounts not found.");

            var sortedQuery = _queryProcessor.SortQuery(baseQuery.AsQueryable(), query.SortBy, query.SortDirection);
            var pagedDiscounts = _queryProcessor.PageQuery(sortedQuery.AsEnumerable(), query.PageNumber, query.PageSize);

            var discountListDto = DiscountDto.CreateDtoFromObjects(pagedDiscounts);

            var pagedDiscountList = new PagedList<DiscountDto>(discountListDto,
                                                               baseQuery.Count(),
                                                               query.PageNumber,
                                                               query.PageSize);

            return pagedDiscountList;
        }
    }
}
