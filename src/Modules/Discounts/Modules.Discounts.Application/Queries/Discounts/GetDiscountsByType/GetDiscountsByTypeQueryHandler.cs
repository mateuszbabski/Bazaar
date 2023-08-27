using MediatR;
using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using NSubstitute;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Modules.Discounts.Application.Queries.Discounts.GetDiscountsByType
{
    public class GetDiscountsByTypeQueryHandler : IRequestHandler<GetDiscountsByTypeQuery, PagedList<DiscountDto>>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IQueryProcessor<Discount> _queryProcessor;

        public GetDiscountsByTypeQueryHandler(IDiscountRepository discountRepository, IQueryProcessor<Discount> queryProcessor)
        {
            _discountRepository = discountRepository;
            _queryProcessor = queryProcessor;
        }
        public async Task<PagedList<DiscountDto>> Handle(GetDiscountsByTypeQuery query, CancellationToken cancellationToken)
        {
            var baseQuery = await _discountRepository.GetDiscountsByType(query.DiscountType, query.DiscountTargetId)
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
