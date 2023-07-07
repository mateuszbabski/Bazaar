using MediatR;
using Modules.Products.Application.Dtos;
using Shared.Application.Queries;

namespace Modules.Products.Application.Queries.GetProductsByShopId
{
    public class GetProductsByShopIdQuery : IRequest<PagedList<ProductDto>>
    {
        #nullable enable
        public Guid ShopId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string SortDirection { get; set; } = "ASC";
    }
}
