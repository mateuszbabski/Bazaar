using MediatR;
using Modules.Shippings.Application.Dtos;
using Shared.Application.Queries;

namespace Modules.Shippings.Application.Queries.ShippingMethods.GetShippingMethods
{
    public class GetAllShippingMethodsQuery : IRequest<PagedList<ShippingMethodDto>>
    {
        #nullable enable
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string SortDirection { get; set; } = "ASC";
    }
}
