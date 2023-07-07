using MediatR;
using Modules.Products.Application.Dtos;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Repositories;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Modules.Products.Application.Queries.GetProductsByName
{
    public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, PagedList<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IQueryProcessor<Product> _queryProcessor;

        public GetProductsByNameQueryHandler(IProductRepository productRepository, IQueryProcessor<Product> queryProcessor)
        {
            _productRepository = productRepository;
            _queryProcessor = queryProcessor;
        }
        public async Task<PagedList<ProductDto>> Handle(GetProductsByNameQuery query, CancellationToken cancellationToken)
        {
            var baseQuery = await _productRepository.GetProductsByName(query.ProductName)
            ?? throw new NotFoundException("Shops not found");

            var sortedQuery = _queryProcessor.SortQuery(baseQuery.AsQueryable(), query.SortBy, query.SortDirection);
            var pagedProducts = _queryProcessor.PageQuery(sortedQuery.AsEnumerable(), query.PageNumber, query.PageSize);

            var productListDto = ProductDto.CreateDtoFromObject(pagedProducts);

            var pagedProductList = new PagedList<ProductDto>(productListDto,
                                                             baseQuery.Count(),
                                                             query.PageNumber,
                                                             query.PageSize);

            return pagedProductList;
        }
    }
}
