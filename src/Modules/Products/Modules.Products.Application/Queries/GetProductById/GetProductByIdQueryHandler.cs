using MediatR;
using Modules.Products.Application.Dtos;
using Modules.Products.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Products.Application.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDetailsDto> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(query.Id)
                ?? throw new NotFoundException("Product not found.");

            var productDto = ProductDetailsDto.CreateDtoFromObject(product);

            return productDto;
        }
    }
}
