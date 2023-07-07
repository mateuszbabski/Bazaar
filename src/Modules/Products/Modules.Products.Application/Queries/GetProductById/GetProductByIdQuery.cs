using MediatR;
using Modules.Products.Application.Dtos;

namespace Modules.Products.Application.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
