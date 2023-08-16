using MediatR;
using Modules.Shippings.Application.Dtos;
using Modules.Shippings.Domain.Entities;
using Modules.Shippings.Domain.Repositories;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Modules.Shippings.Application.Queries.ShippingMethods.GetShippingMethods
{
    public class GetAllShippingMethodsQueryHandler : IRequestHandler<GetAllShippingMethodsQuery, PagedList<ShippingMethodDto>>
    {
        private readonly IShippingMethodRepository _shippingMethodRepository;
        private readonly IQueryProcessor<ShippingMethod> _queryProcessor;

        public GetAllShippingMethodsQueryHandler(IShippingMethodRepository shippingMethodRepository,
                                                 IQueryProcessor<ShippingMethod> queryProcessor)
        {
            _shippingMethodRepository = shippingMethodRepository;
            _queryProcessor = queryProcessor;
        }
        public async Task<PagedList<ShippingMethodDto>> Handle(GetAllShippingMethodsQuery query, CancellationToken cancellationToken)
        {
            var baseQuery = await _shippingMethodRepository.GetShippingMethods() 
                ?? throw new NotFoundException("Shipping methods not found.");
            
            var sortedQuery = _queryProcessor.SortQuery(baseQuery.AsQueryable(), query.SortBy, query.SortDirection);
            
            var pagedShippingMethods = _queryProcessor.PageQuery(sortedQuery.AsEnumerable(), query.PageNumber, query.PageSize);

            var shippingMethodListDto = ShippingMethodDto.CreateDtoListFromObjects(pagedShippingMethods);

            var pagedShippingMethodList = new PagedList<ShippingMethodDto>(shippingMethodListDto,
                                                                           baseQuery.Count(),
                                                                           query.PageNumber,
                                                                           query.PageSize);

            return pagedShippingMethodList;
        }
    }
}
