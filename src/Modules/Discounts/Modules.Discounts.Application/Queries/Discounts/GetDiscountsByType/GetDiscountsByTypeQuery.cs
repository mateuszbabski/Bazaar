using MediatR;
using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Domain.ValueObjects;
using Shared.Application.Queries;

namespace Modules.Discounts.Application.Queries.Discounts.GetDiscountsByType
{
    public class GetDiscountsByTypeQuery : IRequest<PagedList<DiscountDto>>
    {
        #nullable enable
        public DiscountType DiscountType { get; set; }
        public Guid? DiscountTargetId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string SortDirection { get; set; } = "ASC";
    }
}
