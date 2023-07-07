using MediatR;
using Modules.Products.Application.Dtos;
using Shared.Application.Queries;

namespace Modules.Products.Application.Queries.GetProductsByName
{
    public class GetProductsByNameQuery : IRequest<PagedList<ProductDto>>
    {
        #nullable enable
        public string? ProductName { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string SortDirection { get; set; } = "ASC";
    }
}
