﻿using MediatR;
using Modules.Products.Application.Dtos;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Repositories;
using Shared.Abstractions.Queries;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Modules.Products.Application.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, PagedList<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IQueryProcessor<Product> _queryProcessor;

        public GetProductsByCategoryQueryHandler(IProductRepository productRepository, IQueryProcessor<Product> queryProcessor)
        {
            _productRepository = productRepository;
            _queryProcessor = queryProcessor;
        }

        public async Task<PagedList<ProductDto>> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var baseQuery = await _productRepository.GetProductsByCategory(query.CategoryName)
                ?? throw new NotFoundException("Products not found");

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
