using MediatR;
using Modules.Shops.Application.Dtos;
using Shared.Application.Queries;

namespace Modules.Shops.Application.Queries.GetShopsByLocalization
{
    public class GetShopsByLocalizationQuery : IRequest<PagedList<ShopDto>>
    {
        #nullable enable
        public string? Country { get; set; }
        public string? City { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string SortDirection { get; set; } = "ASC";
    }
}
