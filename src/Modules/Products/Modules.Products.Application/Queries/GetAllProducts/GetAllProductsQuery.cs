using MediatR;
using Modules.Products.Application.Dtos;
using Shared.Application.Queries;

namespace Modules.Products.Application.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<PagedList<ProductDto>>
    {
        #nullable enable
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string SortDirection { get; set; } = "ASC";
    }
}
